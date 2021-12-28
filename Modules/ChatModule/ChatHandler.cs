using AltV.Net;
using AltV.Net.Async;
using AltV.Net.Elements.Entities;
using GVRPALTV.Modules.DeathModule;
using GVRPALTV.PlayerHandling;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GVRPALTV.Modules.ChatModule
{
    class ChatHandler : IScript
    {
        [AsyncClientEvent("ChatModule:enter")]
        public async Task ChatEnter(DBPlayer player, string command)
        {
            string[] cmd = command.Split(" ");

            if (cmd[0] == "/pos")
            {
                player.Position = new AltV.Net.Data.Position(Int32.Parse(cmd[1]), Int32.Parse(cmd[2]), Int32.Parse(cmd[3]));
            }
            if (cmd[0] == "/shit")
            {
                player.Emit("Durchfall");
            }
            if (cmd[0] == "/coord")
            {
                Console.WriteLine(player.Position + " | " + player.Rotation);
            }
            if (cmd[0] == "/idgun")
            {
                await player.GiveWeaponAsync(453432689, 9999, true);
                player.Emit("idgun");
            }
            if (cmd[0] == "/revive")
              {

                KillPlayer.revivePlayer(player);
                }
            if (cmd[0] == "/veh")
            {


                   IVehicle veh = Alt.CreateVehicle(Alt.Hash(cmd[1]), new AltV.Net.Data.Position(player.Position.X, player.Position.Y, player.Position.Z), player.Rotation);
                  if (veh != null)
                   {
                       player.SetIntoVehicle(veh, 1);
                    await veh.SetNumberplateTextAsync("ADMIN");
                   
                   }
                    else
                    {
                    await player.ShowNotification("Spawn nicht möglich! :(");
                    }
                    }
            }
        }
}
