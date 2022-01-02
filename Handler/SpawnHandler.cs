using AltV.Net;
using AltV.Net.Data;
using AltV.Net.Elements.Entities;
using GVRPALTV.DatenbankHandling;
using GVRPALTV.Modules.ClothingModule;
using GVRPALTV.Modules.Clothingstoremodule;
using GVRPALTV.Modules.DeathModule;
using GVRPALTV.PlayerHandling;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace GVRPALTV.Handler
{
    public class SpawnHandler : IScript
    {
        public static async Task spawnPlayer(DBPlayer player)
        {
            using MySQLHandler db = new MySQLHandler();
            var dbPlayer = db.PlayerCharacter.ToList().FirstOrDefault(account => (account.name == player.Name));
            if (dbPlayer == null)
            {
                Console.WriteLine("Fehler! Diesen Benutzer gibt es nicht! | " + player.Name);
                return;
            }
            player.loggedin = true;
            player.accountid = dbPlayer.id;
            player.forumid = dbPlayer.forumid;
            player.name = dbPlayer.name;
            player.adminlevel = dbPlayer.adminlevel;
            player.socialclub = dbPlayer.socialclub;
            player.money = dbPlayer.money;
            player.blackmoney = dbPlayer.blackmoney;
            player.bank = dbPlayer.bank;
            player.firstname = dbPlayer.firstname;
            player.lastname = dbPlayer.lastname;
            player.visum = dbPlayer.visum;
            player.playhours = dbPlayer.playhours;
            player.health = dbPlayer.health;
            player.Health = (ushort)dbPlayer.health;

            player.armor = dbPlayer.armor;
            player.Armor = (ushort)dbPlayer.armor;
            player.inventory = dbPlayer.inventory;
            player.gender = dbPlayer.gender;
            player.bewusstlos = dbPlayer.bewusstlos;
            player.deathtime = dbPlayer.deathtime;
            player.hunger = dbPlayer.hunger;
            player.durst = dbPlayer.durst;
            player.adresse = dbPlayer.adresse;
            player.jail = dbPlayer.jail;
            player.jailtime = dbPlayer.jailtime;
            player.klogang = dbPlayer.klogang;
            player.lastlogin = dbPlayer.lastlogin;
            player.phone = dbPlayer.phone;
            player.fraktion = dbPlayer.fraktion;
            player.fraktion_rank = dbPlayer.fraktion_rank;

            player.clothes = dbPlayer.clothes;
            player.restclothes = dbPlayer.restclothes;

            if (player.clothes == "" || player.clothes == "[]" || player.clothes == null)
            {
                var data = JsonConvert.DeserializeObject<ClothingModule>("{\"Mask\":0}");

                data.Hose = 1;
                data.HoseColor = 0;
                data.Mask = 0;
                data.TShirt = 1;
                data.TShirtColor = 0;
                data.Torso = 1;
                data.TorsoColor = 0;
                data.Koerper = 0;
                data.Schuhe = 1;
                data.SchuheColor = 0;




                var final = JsonConvert.SerializeObject(data);
                dbPlayer.clothes = final;
                player.clothes = final;
                await db.SaveChangesAsync();
            }
            if (player.restclothes == "" || player.restclothes == "[]" || player.restclothes == null)
            {
                var data = JsonConvert.DeserializeObject<ClothingModule>("{\"Mask\":0}");

                data.Hose = 1;
                data.HoseColor = 0;
                data.Mask = 0;
                data.TShirt = 1;
                data.TShirtColor = 0;
                data.Torso = 1;
                data.TorsoColor = 0;
                data.Koerper = 0;
                data.Schuhe = 1;
                data.SchuheColor = 0;




                var final = JsonConvert.SerializeObject(data);
                dbPlayer.restclothes = final;
                player.restclothes = final;
                await db.SaveChangesAsync();
            }

            player.pos_X = dbPlayer.pos_X;
            player.pos_Y = dbPlayer.pos_Y;
            player.pos_Z = dbPlayer.pos_Z;
            player.Position = new Position((float)player.pos_X, (float)player.pos_Y, (float)player.pos_Z);
            player.Dimension = 0;
            await Task.Delay(100);


            if (player.bewusstlos)
            {
                KillPlayer.killPlayer(player);
            } else
            {
                player.Emit("deathlockplayer", false);
                player.Emit("reviveden");

            }

           await ClothingstoreInteraction.SetClothes(player);


            //   IVehicle veh = Alt.CreateVehicle(Alt.Hash("revolter"), new AltV.Net.Data.Position(player.Position.X, player.Position.Y, player.Position.Z), player.Rotation);
            //   if (veh != null)
            //   {
            //        player.SetIntoVehicle(veh, 1);
            //   }
            //    else
            //    {
            //    }




        }
    }
}
