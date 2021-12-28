using GVRPALTV.PlayerHandling;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GVRPALTV.Modules.DeathModule
{
    class KillPlayer
    {
        public static async Task killPlayer(DBPlayer player)
        {
            player.bewusstlos = true;
            player.deathtime = 600000;
            player.ClearBloodDamage();
            player.Emit("hudhide");
            player.Emit("deathlockplayer", true);
            
        }

        public static async Task revivePlayer(DBPlayer player)
        {
            player.Health = 200;
            player.bewusstlos = false;
            player.deathtime = 0;
            player.hunger = 200;
            player.durst = 200;
            player.Emit("revive");
            player.Emit("deathlockplayer", false);
            player.Emit("hudready");

        }
    }
   }