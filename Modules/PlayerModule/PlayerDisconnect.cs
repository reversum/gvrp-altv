using AltV.Net;
using AltV.Net.Async;
using GVRPALTV.DatenbankHandling;
using GVRPALTV.PlayerHandling;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GVRPALTV.Modules.PlayerModule
{
    class PlayerDisconnect : IScript
    {
        [AsyncScriptEvent(ScriptEventType.PlayerDisconnect)]
        public async Task PlayerDisconne(DBPlayer user, string reason)
        {


            await using MySQLHandler db = new MySQLHandler();


                var dbPlayer = db.PlayerCharacter.ToList().FirstOrDefault(account => (account.socialclub == user.SocialClubId));
                if (dbPlayer == null)
                {
                    Console.WriteLine("Fehler! Diesen Benutzer gibt es nicht! | " + user.SocialClubId);
                    return;
                }
                dbPlayer.id = user.accountid;
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


            await db.SaveChangesAsync();
        }

    }
}
