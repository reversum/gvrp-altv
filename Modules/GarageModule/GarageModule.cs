using AltV.Net;
using AltV.Net.Async;
using GVRPALTV.PlayerHandling;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GVRPALTV.Utils;
using GVRPALTV.Modules.NativeMenu;
using GVRPALTV.DatenbankHandling;
using static GVRPALTV.Modules.NativeMenu.DialogMigrator;
using AltV.Net.Data;
using GVRPALTV.VehicleHandling;

namespace GVRPALTV.Modules.GarageModule
{
    class GarageModule : IScript
    {
        [AsyncClientEvent("action")]
        public async Task Parkout(DBPlayer player, string action, string data)
        {

            if (action == "garage:parkout")
            {
                DialogMigrator.CloseUserMenu(player, 44);


                string[] cmd = data.Split(",");
                using MySQLHandler db = new MySQLHandler();
                var garage = db.GarageHandler.ToList().FirstOrDefault(garages => (garages.name == cmd[0]));
                int vehicleid = Int32.Parse(cmd[1]);
                var veh = db.VehicleHandler.ToList().FirstOrDefault(vehicles => (vehicles.id == vehicleid));
                await player.ShowNotification("Dein Fahrzeug mit dem Kennzeichen ~g~" + veh.plate + " ~s~ ausgeparkt!");

                veh.ingarage = false;
                await db.SaveChangesAsync();

                var spawnableVehicle = await AltAsync.Do(() => Alt.CreateVehicle((uint)veh.hash, new AltV.Net.Data.Position(garage.spawnpos_x, garage.spawnpos_y, garage.spawnpos_z), new Rotation(0, 0, garage.spawnheading)));

                spawnableVehicle.NumberplateText = veh.plate;

                spawnableVehicle.SetVehicleOwner(veh.ownerid);
                spawnableVehicle.SetVehicleId(veh.id);
                if (veh.engine)
                {
                    spawnableVehicle.EngineOn = true;
                }
                else
                {
                    spawnableVehicle.EngineOn = false;
                }
                if (veh.locked)
                {
                    spawnableVehicle.LockState = AltV.Net.Enums.VehicleLockState.Locked;
                }
                else
                {
                    spawnableVehicle.LockState = AltV.Net.Enums.VehicleLockState.Unlocked;

                }
                foreach (var fahrzeug in GetAllVehicles())
                {

                    if (fahrzeug.id == veh.id)
                    {
                        fahrzeug.id = veh.id;
                        fahrzeug.name = veh.name;
                        fahrzeug.plate = veh.plate;
                        fahrzeug.hash = veh.hash;
                        fahrzeug.price = veh.price;
                        fahrzeug.trunk = veh.trunk;
                        fahrzeug.trunkweight = veh.trunkweight;
                        fahrzeug.MaxFuel = veh.MaxFuel;
                        fahrzeug.pos_X = veh.pos_X;
                        fahrzeug.pos_Y = veh.pos_Y;
                        fahrzeug.pos_Z = veh.pos_Z;
                        fahrzeug.rotation = veh.rotation;
                        fahrzeug.tuning = veh.tuning;
                        fahrzeug.engine = veh.engine;
                        fahrzeug.locked = veh.locked;
                        fahrzeug.health = veh.health;
                        fahrzeug.angemeldet = veh.angemeldet;
                        fahrzeug.ingarage = false;
                        veh.ingarage = false;

                    }
                }
            }
        }

        public static IEnumerable<DBVehicle> GetAllVehicles()
        {
            return Alt.GetAllVehicles().Cast<DBVehicle>();
        }
        [AsyncClientEvent("action")]
        public async Task Parkein(DBPlayer player, string action, string data)
        {
            if (action == "garage:parkein")
            {
                DialogMigrator.CloseUserMenu(player, 44);

                int vehicleid = Int32.Parse(data);
                foreach (var fahrzeug in GetAllVehicles())
                {

                    if (fahrzeug.id == vehicleid)
                    {
                        fahrzeug.ingarage = true;
                        using MySQLHandler db = new MySQLHandler();

                        var dbVehicle = db.VehicleHandler.ToList().FirstOrDefault(vehicle => (vehicle.id == fahrzeug.GetVehicleOwner()));
                        if (dbVehicle != null)
                        {
                            dbVehicle.ingarage = true;
                            await db.SaveChangesAsync();
                            await fahrzeug.RemoveAsync();
                            await player.ShowNotification("Dein Fahrzeug mit dem Kennzeichen ~g~" + dbVehicle.plate + " ~s~ eingeparkt!");

                        }


                    }
                }
            }
            if (action == "garage:einparken")
            {

                using MySQLHandler db = new MySQLHandler();

                var garage = db.GarageHandler.ToList().FirstOrDefault(garages => (garages.name == data));

                var vehicleList = Alt.GetAllVehicles().Where(vehicle => vehicle.Exists && vehicle.HasVehicleOwner() && vehicle.GetVehicleId() > 0 && vehicle.Position.IsInRange(player.Position, 50f)).ToList();
                List<Item> Items = new List<Item>();

                int id = 1; 
                foreach (var vehicle in vehicleList)
                {
                    if (vehicle.GetVehicleOwner() == player.accountid)
                    {
                        Items.Add(new Item(id, vehicle.NumberplateText, "", "garage:parkein", "" + vehicle.GetVehicleId(), false, 0, false, false));
                        id = id + 1;
                    }
                }

                DialogMigrator.CreateMenu(player, 44, "Garage " + garage.name, "", Items);

                DialogMigrator.ShowMenu(player, 44);
            }
        }
    }
}
