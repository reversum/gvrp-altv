using AltV.Net;
using AltV.Net.Elements.Entities;
using GVRPALTV.PlayerHandling;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace GVRPALTV.Modules.BeduerfnisseModule
{
    class BeduerfnisseTimer
    {
        public static void OnHunger(object sender, ElapsedEventArgs e)
        {

            foreach (var player in GetAllPlayers())
            {



                if (!player.bewusstlos || player.loggedin)
                {
                    player.hunger -= 2;
                }

                if (player.hunger == 50)
                {
                    player.ShowNotification("Dein Magen grummelt leise... Du solltest mit der Zeit etwas zu essen besorgen.");
                }
                else if (player.hunger == 47)
                {
                    // keine Nachricht
                }
                else if (player.hunger == 40)
                {
                    player.ShowNotification("Du verspürst starken Hunger. Suche dir am besten schnell Nahrung!");

                }
                else if (player.hunger == 35)
                {
                    // keine Nachricht
                }
                else if (player.hunger == 24)
                {
                    player.ShowNotification("Dein Magen grummelt gewaltig laut! Suche dir sofort Nahrung sonst wird es brennzlich!");
                }
                else if (player.hunger == 20)
                {
                    // keine Nachricht
                }
                else if (player.hunger == 12)
                {
                    player.ShowNotification("Du merkst wie dir immer mehr schwarz vor ~r~Augen~s~ wird!");
                    player.Emit("startKreislauf");

                }
                else if (player.hunger == 12)
                {
                    // keine Nachricht
                } else if (player.hunger == 0)
                {
                    player.Health = 0;
                   
                }
            }
        }
        public static void OnKlo(object sender, ElapsedEventArgs e)
        {

            foreach (var player in GetAllPlayers())
            {


                if (!player.bewusstlos || player.loggedin)
                {
                    player.klogang += 1;
                    if (player.klogang == 100)
                    {
                        player.ShowNotification("~r~Das ging in die Hose~s~...");
                        player.klogang = 0;
                        player.Emit("Durchfall");

                    }
                    if (player.klogang == 50)
                    {
                        player.ShowNotification("Dein Magen grummelt verdächtig... Musst du langsam auf die Toilette?");
                    }
                    if (player.klogang == 75)
                    {
                        player.ShowNotification("Langsam wird es echt dringend! Ich sollte mir schnell eine Toilette suchen!");
                    }
                    if (player.klogang == 95)
                    {
                        player.ShowNotification("Ich halte es nicht länger aus!");
                    }
                }

                }
        }
        public static void OnDurst(object sender, ElapsedEventArgs e)
        {

            foreach (var player in GetAllPlayers())
            {





                if (!player.bewusstlos || player.loggedin)
                {
                    player.durst -= 2;
                }
                if (player.durst == 80)
                {
                    player.ShowNotification("Ich könnte so langsam mal was zu trinken gebrauchen...");

                }
                if (player.durst == 50)
                {
                    player.ShowNotification("Deine Kehle ist trocken. Hast du zufällig noch etwas zu trinken dabei?");
                }
                else if (player.durst == 47)
                {
                    // keine Nachricht
                }
                else if (player.durst == 40)
                {
                    player.ShowNotification("Meine Kehle ist so trocken... Ich brauche so langsam wirklich was zu trinken...");

                }
                else if (player.durst == 35)
                {
                    // keine Nachricht
                }
                else if (player.durst == 24)
                {
                    player.ShowNotification("Ich glaube so langsam schaff ich das nicht mehr... Wasser.. Irgendwas.. bitte...");
                }
                else if (player.durst == 20)
                {
                    // keine Nachricht
                }
                else if (player.durst == 12)
                {
                    player.ShowNotification("Du merkst wie dir immer mehr schwarz vor ~r~Augen~s~ wird!");
                    player.Emit("startKreislauf");

                }
                else if (player.durst == 12)
                {
                    // keine Nachricht
                }
                else if (player.durst == 0)
                {
                    player.Health = 0;

                }
            }
        }
        public static IEnumerable<DBPlayer> GetAllPlayers()
            {
                return Alt.GetAllPlayers().Cast<DBPlayer>();
            }
        }
    
}