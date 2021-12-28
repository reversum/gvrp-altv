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

namespace GVRPALTV.Modules.WorkingModule.BurgerShot
{
    class BurgerShotLivery : IScript
    {


        public static async Task StartJob(DBPlayer player)
        {
            player.minijobcount = 0;
            player.minijobmax = 10;



            await ChooseRoute(player);

            var spawnableVehicle = await AltAsync.Do(() => Alt.CreateVehicle(Alt.Hash("faggio2"), new AltV.Net.Data.Position((float)-1175.3671, (float)-883.978, (float)13.9296875), new Rotation(0, 0, (float)0.64)));
            spawnableVehicle.NumberplateText = "FOOD";
            spawnableVehicle.SetVehicleOwner(player.Id);
            spawnableVehicle.SetVehicleId(0);

            spawnableVehicle.EngineOn = false;



            spawnableVehicle.LockState = AltV.Net.Enums.VehicleLockState.Locked;



        }
        public static async Task ChooseRoute(DBPlayer player)
        {
            Position lieferung;

            using MySQLHandler db = new MySQLHandler();
            Random random = new Random();
            int ss = random.Next(1, 13);
            var result = db.MiniJobDeliveryHandler
                                  .Where(c => c.jobname == player.currentminijob)

                                  .OrderBy(c => Guid.NewGuid())
                                  .Skip(ss).FirstOrDefault();

           await CreateRoute(player, new Position(result.pos_x, result.pos_y, result.pos_z));


        }
        public static async Task CreateRoute(DBPlayer player, Position position)
        {
            AltInteractions.AddInteraction(new Interaction(1, 1, position, 0, 1));
            var blip = player.Server.CreateBlip(player, 4, position);
            blip.Color = 81;
            blip.Route = true;
            blip.Name = "Lieferung";
            player.minijobblip = blip;


            var marker = MarkerManager.MarkerStreamer.Create(1,
position,
new Vector3(1, 1, 1), null, null,
new Rgba((byte)255, (byte)136, (byte)0, (byte)100), 0, false);

            player.minijobmarker = marker;

        }
    }
}
