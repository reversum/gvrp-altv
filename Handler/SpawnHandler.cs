using AltV.Net;
using AltV.Net.Data;
using AltV.Net.Elements.Entities;
using GVRPALTV.DatenbankHandling;
using GVRPALTV.Modules.DeathModule;
using GVRPALTV.PlayerHandling;
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
            var dbPlayer = db.PlayerCharacter.ToList().FirstOrDefault(account => (account.socialclub == player.SocialClubId));
            if (dbPlayer == null)
            {
                Console.WriteLine("Fehler! Diesen Benutzer gibt es nicht! | " + player.SocialClubId);
                return;
            }

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

            player.pos_X = dbPlayer.pos_X;
            player.pos_Y = dbPlayer.pos_Y;
            player.pos_Z = dbPlayer.pos_Z;
            player.Position = new Position((float)player.pos_X, (float)player.pos_Y, (float)player.pos_Z);
            player.Dimension = 0;
            await Task.Delay(100);

            player.Emit("loadallblips", BlipHandler.GetAllServerBlipsInJson());

            if (player.bewusstlos)
            {
                KillPlayer.killPlayer(player);
            } else
            {
                player.Emit("deathlockplayer", false);
                player.Emit("reviveden");

            }

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
