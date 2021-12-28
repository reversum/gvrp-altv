using AltV.Net;
using AltV.Net.Async;
using GVRPALTV.PlayerHandling;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GVRPALTV.Modules.BeduerfnisseModule
{
    class Scheißhaus : IScript
    {
        [AsyncClientEvent("Beduerfnisse:MussKacken")]
        public async Task KloGang(DBPlayer player)
        {
            Random rnd = new Random();
            int wait = rnd.Next(400, 2000);
            await Task.Delay(wait);

            if (player.klogang > 50)
            {
                await player.ShowNotification("Das tat gut... ~g~Klogang ~s~abgeschlossen.");
                player.klogang = 0;

            }
            else
            {
                await player.ShowNotification("Ich muss noch nicht auf Klo!");
            }

        }
    }
}
