using AltV.Net;
using GVRPALTV.PlayerHandling;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace GVRPALTV.Modules.DeathModule
{
    class DeathTimer
    {
        public static async void OnDeathTimer(object sender, ElapsedEventArgs e)
        {
            foreach (var player in GetAllPlayers())
            {



                if (player.bewusstlos)
                {
                    player.deathtime -= 5000;
                    player.Emit("deathlockplayer", true);
                    


                    if (player.deathtime == 0 || player.deathtime < 0)
                    {
                        KillPlayer.revivePlayer(player);

                        await Task.Delay(100);

                   //     player.Position = 

                    }
                }
            }
            }

        public static IEnumerable<DBPlayer> GetAllPlayers()
        {
            return Alt.GetAllPlayers().Cast<DBPlayer>();
        }
    }
    }
