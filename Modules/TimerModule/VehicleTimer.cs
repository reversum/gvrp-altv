using AltV.Net;
using GVRPALTV.DatenbankHandling;
using GVRPALTV.PlayerHandling;
using GVRPALTV.VehicleHandling;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace GVRPALTV.Modules.TimerModule
{
    class VehicleTimer
    {
 
        public static async void OnVehicleTimer(object sender, ElapsedEventArgs e)
        {
            await using MySQLHandler db = new MySQLHandler();
            int count = Alt.GetAllVehicles().Count;
            Alt.Log($"[Vehicle Saved Count] {count}");
            foreach (var fahrzeug in GetAllVehicles())
            {

                var dbVehicle = db.VehicleHandler.ToList().FirstOrDefault(vehicle => (vehicle.id == fahrzeug.id));
                if (dbVehicle != null)
                {
                
                dbVehicle.id = fahrzeug.id;
                dbVehicle.ownerid = fahrzeug.ownerid;
                dbVehicle.name = fahrzeug.name;
                dbVehicle.plate = fahrzeug.plate;
                dbVehicle.hash = fahrzeug.hash;
                dbVehicle.price = fahrzeug.price;
                dbVehicle.trunk = fahrzeug.trunk;
                dbVehicle.trunkweight = fahrzeug.trunkweight;
                dbVehicle.MaxFuel = fahrzeug.MaxFuel;
                fahrzeug.pos_X = fahrzeug.Position.X;
                fahrzeug.pos_Y = fahrzeug.Position.Y;
                fahrzeug.pos_Z = fahrzeug.Position.Z;
                fahrzeug.rotation = fahrzeug.Rotation.Yaw;

                dbVehicle.pos_X = fahrzeug.pos_X;
                dbVehicle.pos_Y = fahrzeug.pos_Y;
                dbVehicle.pos_Z = fahrzeug.pos_Z;
                dbVehicle.rotation = fahrzeug.rotation;
                dbVehicle.tuning = fahrzeug.tuning;
                dbVehicle.engine = fahrzeug.EngineOn;
                dbVehicle.locked = fahrzeug.locked;
                    dbVehicle.angemeldet = fahrzeug.angemeldet;
                    dbVehicle.ingarage = fahrzeug.ingarage;

                    Alt.Log($"[Vehicle Saved] {fahrzeug.id} | {fahrzeug.plate}");
                }
            }

            await db.SaveChangesAsync();


        }
        public static IEnumerable<DBVehicle> GetAllVehicles()
        {
            return Alt.GetAllVehicles().Cast<DBVehicle>();
        }
    }
    }
