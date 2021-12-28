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

            Random rnd = new Random();
            int ort = rnd.Next(1, 1);
            switch (ort)
            {
                case 1:
                    lieferung = new Position((float)-935.644, (float)-1523.0637, (float)4.2351074);
                    await CreateRoute(player, lieferung);


                    break;
                case 2:
                    break;
            }
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
