using AltV.Net;
using AltV.Net.Async;
using AltV.Net.Data;
using GVRPALTV.VehicleHandling;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GVRPALTV.Utils;
using GVRPALTV.PlayerHandling;
using AltV.Net.Elements.Entities;

namespace GVRPALTV.DatenbankHandling
{
    public partial class VehicleHandler : IScript
    {
        public int id { get; set; }
        public int ownerid { get; set; }
        public string plate { get; set; }
        public string name { get; set; }
        public ulong hash { get; set; }
        public float price { get; set; }
        public string trunk { get; set; }
        public int trunkweight { get; set; }
        public int MaxFuel { get; set; }
        public float pos_X { get; set; }
        public float pos_Y { get; set; }
        public float pos_Z { get; set; }
        public float rotation { get; set; }
        public string tuning { get; set; }
        public bool engine { get; set; }
        public bool locked { get; set; }
        public int health { get; set; }

        public static async Task LoadAllVehiclesFromDb()
        {
            using MySQLHandler db = new MySQLHandler();
            foreach (var veh in db.VehicleHandler)
            {

                var spawnableVehicle = await AltAsync.Do(() => Alt.CreateVehicle((uint)veh.hash, new AltV.Net.Data.Position(veh.pos_X, veh.pos_Y, veh.pos_Z), new Rotation(0, 0, veh.rotation)));

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
                    }
                }

            }
        }
        public static IEnumerable<DBVehicle> GetAllVehicles()
        {
            return Alt.GetAllVehicles().Cast<DBVehicle>();
        }
        [AsyncClientEvent("Pressed_G")]
        public async Task Pressed_G(DBPlayer player, DBVehicle vehicle)
        {

            if(vehicle.GetVehicleOwner() != player.accountid) { return; }
            if (!vehicle.locked)
            {
                vehicle.locked = true;

                vehicle.LockState = AltV.Net.Enums.VehicleLockState.Locked;
                await player.ShowNotification("Du hast dein Fahrzeug ~r~abgeschlossen~s~.");
            }
            else
            {
                vehicle.locked = false;

                vehicle.LockState = AltV.Net.Enums.VehicleLockState.Unlocked;
                await player.ShowNotification("Du hast dein Fahrzeug ~g~aufgeschlossen~s~.");

            }
        }
        [AsyncClientEvent("Pressed_M")]
        public async Task Pressed_M(DBPlayer player, DBVehicle vehicle)
        {
            if (vehicle.EngineOn)
            {
                vehicle.EngineOn = false;
                vehicle.engine = false;

                await player.ShowNotification("Motor: ~r~gestoppt~s~.");
            }
            else
            {
                vehicle.EngineOn = true;
                vehicle.engine = true;

                await player.ShowNotification("Motor: ~g~gestartet~s~.");

            }
        }
    }
}
