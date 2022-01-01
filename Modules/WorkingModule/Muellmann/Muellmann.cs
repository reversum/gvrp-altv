using AltV.Net;
using AltV.Net.Async;
using AltV.Net.Data;
using AltV.Net.Interactions;
using GVRPALTV.PlayerHandling;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using GVRPALTV.Utils;
using System.Drawing;
using GVRPALTV.EntitySync;
using GVRPALTV.DatenbankHandling;

namespace GVRPALTV.Modules.WorkingModule.Muellmann
{
    class Muellmann : IScript
    {


        public static async Task StartJob(DBPlayer player)
        {
            player.minijobcount = 0;
            player.minijobmax = 30;



            await ChooseRoute(player);
            // Position(x: -317,14285, y: -1524,7517, z: 27,544312) | Rotation(roll: 0, pitch: 0, yaw: -1,5336909)
            var spawnableVehicle = await AltAsync.Do(() => Alt.CreateVehicle(Alt.Hash("trash"), new AltV.Net.Data.Position((float)-317.14285, (float)-1524.7517, (float)27.544312), new Rotation(0, 0, (float)-1.5336909)));
            spawnableVehicle.NumberplateText = "DUMP";
            spawnableVehicle.SetVehicleOwner(player.accountid);
            spawnableVehicle.SetVehicleId(0);

            spawnableVehicle.EngineOn = false;



            spawnableVehicle.LockState = AltV.Net.Enums.VehicleLockState.Locked;



        }
        public static async Task ChooseRoute(DBPlayer player)
        {
            Position lieferung;

            using MySQLHandler db = new MySQLHandler();
            Random random = new Random();
            int ss = random.Next(1, 40);
            var result = db.MiniJobDeliveryHandler
                                  .Where(c => c.jobname == player.currentminijob)

                                  .OrderBy(c => Guid.NewGuid())
                                  .Skip(ss).FirstOrDefault();

            await CreateRoute(player, new Position(result.pos_x, result.pos_y, result.pos_z));


        }
        public static async Task CreateRoute(DBPlayer player, Position position)
        {
            AltInteractions.AddInteraction(new Interaction(1, 2, position, 0, 2));
            var blip = player.Server.CreateBlip(player, 4, position);
            blip.Color = 81;
            blip.Route = true;
            blip.Name = "Mülltonne";
            player.minijobblip = blip;


            var marker = MarkerManager.MarkerStreamer.Create(1,
position,
new Vector3(1, 1, 1), null, null,
new Rgba((byte)255, (byte)136, (byte)0, (byte)100), 0, false);

            player.minijobmarker = marker;

        }
    }
}
