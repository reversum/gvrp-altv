using AltV.Net;
using GVRPALTV.DatenbankHandling;
using GVRPALTV.PlayerHandling;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace GVRPALTV.Modules.TimerModule
{
    class PlayerTimer
    {
        public static void OnTimeSyncTimer(object sender, ElapsedEventArgs e)
        {
            var currenttime = DateTime.Now;
            foreach (var player in Alt.GetAllPlayers())
            {
                player.SetDateTime(currenttime.Day, currenttime.Month, currenttime.Year, currenttime.Hour, currenttime.Minute, currenttime.Second);
            }
        }

        public static async void OnSyncTimer(object sender, ElapsedEventArgs e)
        {
            await using MySQLHandler db = new MySQLHandler();
            int count = Alt.GetAllPlayers().Count;
            Alt.Log($"[Player Saved Count] {count}");
            foreach (var user in GetAllPlayers())
            {

                var dbPlayer = db.PlayerCharacter.ToList().FirstOrDefault(account => (account.socialclub == user.SocialClubId));
                if (dbPlayer == null)
                {
                    Console.WriteLine("Fehler! Diesen Benutzer gibt es nicht! | " + user.SocialClubId);
                    return;
                }
                dbPlayer.forumid = user.forumid;
                dbPlayer.name = user.name;
                dbPlayer.adminlevel = user.adminlevel;
                dbPlayer.socialclub = user.socialclub;
                dbPlayer.money = user.money;
                dbPlayer.blackmoney = user.blackmoney;
                dbPlayer.bank = user.bank;
                dbPlayer.firstname = user.firstname;
                dbPlayer.lastname = user.lastname;
                dbPlayer.visum = user.visum;
                dbPlayer.playhours = user.playhours;
                dbPlayer.health = user.Health;

                dbPlayer.armor = user.Armor;
                dbPlayer.inventory = user.inventory;
                dbPlayer.gender = user.gender;
                dbPlayer.bewusstlos = user.bewusstlos;
                dbPlayer.deathtime = user.deathtime;
                dbPlayer.hunger = user.hunger;
                dbPlayer.durst = user.durst;
                dbPlayer.adresse = user.adresse;
                dbPlayer.jail = user.jail;
                dbPlayer.jailtime = user.jailtime;
                dbPlayer.klogang = user.klogang;
                dbPlayer.lastlogin = user.lastlogin;
                dbPlayer.phone = user.phone;
                dbPlayer.fraktion = user.fraktion;
                dbPlayer.fraktion_rank = user.fraktion_rank;

                user.pos_X = user.Position.X;
                user.pos_Y = user.Position.Y;
                user.pos_Z = user.Position.Z;

                dbPlayer.pos_X = user.pos_X;
                dbPlayer.pos_Y = user.pos_Y;
                dbPlayer.pos_Z = user.pos_Z;
                Alt.Log($"[Player Saved] {user.firstname}_{user.lastname}");

            }

            Alt.Log($"[DBSAVED] ALL!");
            await db.SaveChangesAsync();


        }
        public static IEnumerable<DBPlayer> GetAllPlayers()
        {
            return Alt.GetAllPlayers().Cast<DBPlayer>();
        }
    }
    }
