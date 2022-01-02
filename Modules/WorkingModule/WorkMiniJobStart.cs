using AltV.Net;
using AltV.Net.Async;
using AltV.Net.Data;
using AltV.Net.Interactions;
using GVRPALTV.Modules.NativeMenu;
using GVRPALTV.Modules.WorkingModule.BurgerShot;
using GVRPALTV.PlayerHandling;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GVRPALTV.Modules.WorkingModule.Muellmann;
using static GVRPALTV.Modules.NativeMenu.DialogMigrator;

namespace GVRPALTV.Modules.WorkingModule
{
    public class WorkMiniJobStart : IScript
    {
        public static Position BurgerShotStart = new Position((float)-1178.6505, (float)-891.6528, (float)13.744385);

        [AsyncClientEvent("action")]
        public async Task action(DBPlayer player, String text, String data2)
        {
         //   Console.WriteLine(text + " | " + data2);

        }

        [AsyncClientEvent("garbageisgay")]
        public async Task garbageisgay(DBPlayer player)
        {
            player.minijobblip.Remove();
            player.minijobmarker.Destroy();


            AltInteractions.RemoveInteraction(player.minijobinteraction);
            await Muellmann.Muellmann.ChooseRoute(player);

        }
        [AsyncClientEvent("Pressed_E")]
        public async Task Pressed_E(DBPlayer player)
        {
            if (!player.loggedin) return;

            var interactions = await AltInteractions.FindInteractions(player.Position, 0);
            Console.WriteLine(interactions);

            foreach (var interaction in interactions)
            {
                if (interaction.Type == 1 && interaction.Id == 1)
                {
                    Random rnd = new Random();
                    int trinkgeld = rnd.Next(1, 10);
                    player.minijobcount += 1;
                    await player.ShowAdvancedNotification("~g~Lieferung erfolgreich!~n~~s~Trinkgeld: ~y~ " + trinkgeld + "$~n~Schicht: ~b~" + player.minijobcount + "~s~/~r~" + player.minijobmax, 0, "Burger-Shot", "Job-Benachrichtigung", "CHAR_MOLLY", null, 1);
                    player.money += trinkgeld;
                     player.minijobblip.Remove();
                     player.minijobmarker.Destroy();
                    AltInteractions.RemoveInteraction(interaction); 
                    if (player.minijobcount < player.minijobmax)
                    {
                        await BurgerShotLivery.ChooseRoute(player);

                    } else
                    {
                        Random rnd2 = new Random();
                        int gehalt = rnd2.Next(100, 150);
                        await player.ShowAdvancedNotification("~g~Schicht erfolgreich!~n~~s~Gehalt: ~y~ " + gehalt + "~s~$", 0, "Burger-Shot", "Job-Benachrichtigung", "CHAR_MOLLY", null, 1);
                        player.money += gehalt;

                    }
                    //Burgershot

                }
                if (interaction.Type == 1 && interaction.Id == 2)
                {
                     player.Emit("checkforgarbage", player.minijobveh);
                    player.minijobinteraction = interaction;
                    //Müllmann

                }
                if (interaction.Type == 0 && interaction.Id == 32)
                {
                    if (player.currentminijob == "trash" && player.minijobactive)
                    {
                        await player.ShowAdvancedNotification("~r~Du arbeitest doch bereits hier! Arbeite deine Route ab!", 0, "Los-Santos Garbage", "Job-Benachrichtigung", "CHAR_JIMMY_BOSTON", null, 1);
                    }
                    else
                    {
                        player.currentminijob = "trash";
                        player.minijobactive = true;
                        await Muellmann.Muellmann.StartJob(player);
                        await player.ShowAdvancedNotification("~s~Guten Tag, fahr die Route ab und sammle alle Mülltüten ein!", 0, "Los-Santos Garbage", "Job-Benachrichtigung", "CHAR_JIMMY_BOSTON", null, 1);

                    }
                }
                if (interaction.Type == 0 && interaction.Id == 31)
                {
                    if (player.currentminijob == "burgershot" && player.minijobactive)
                    {
                        await player.ShowAdvancedNotification("~r~Du arbeitest doch bereits hier! Arbeite deine Route ab!", 0, "Burger-Shot", "Job-Benachrichtigung", "CHAR_MOLLY", null, 1);
                    } else
                    {
                        player.currentminijob = "burgershot";
                        player.minijobactive = true;
                        await BurgerShotLivery.StartJob(player);
                        await player.ShowAdvancedNotification("~s~Guten Tag, liefere die ~g~Bestellung ~s~so schnell wie möglich! Ab gehts!", 0, "Burger-Shot", "Job-Benachrichtigung", "CHAR_MOLLY", null, 1);

                    }
                }
            }
        }
}
}
