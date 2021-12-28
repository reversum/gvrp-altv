using AltV.Net;
using AltV.Net.Async;
using AltV.Net.Data;
using AltV.Net.Elements.Entities;
using AltV.Net.Enums;
using GVRPALTV.DatenbankHandling;
using GVRPALTV.EntitySync;
using GVRPALTV.PlayerHandling;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GVRPALTV.Handler
{
    public class JoinHandler : IScript
    {

        [JsonProperty(PropertyName = "name")] private string Name { get; }
        [JsonProperty(PropertyName = "rank")] private uint Rank { get; }


        [AsyncScriptEvent(ScriptEventType.PlayerConnect)]
        public async Task PlayerConnect(DBPlayer player, string reason)
        {

            lock (player)
            {
                if (!player.Exists)
                {
                    return;
                }
                player.accountid = 0;
                player.forumid = 0;

                player.loggedin = false;
            }

            using MySQLHandler db = new MySQLHandler();
            
            var dbPlayer = db.PlayerCharacter.ToList().FirstOrDefault(account => (account.socialclub == player.SocialClubId));
            if (dbPlayer == null)
            {
                Console.WriteLine("Fehler! Diesen Benutzer gibt es nicht! | " + player.SocialClubId);
                return;
            }


            #region
            //      player.accountid = dbPlayer.id;
            //      player.firstname = dbPlayer.firstname;
            //     player.lastname = dbPlayer.lastname;
            //     player.adminlevel = dbPlayer.adminlevel;
            //     player.password = dbPlayer.password;
            //     player.health = dbPlayer.health;

            #endregion
            player.Emit("deathlockplayer", false);

            player.SetDateTime(DateTime.Now);

            if (dbPlayer.gender == 0)
            {
                await player.SetModelAsync((uint)PedModel.FreemodeMale01);
            } else
            {
                await player.SetModelAsync((uint)PedModel.FreemodeFemale01);

            }

            await player.SpawnAsync(new Position((float)17.4809, (float)637.872, (float)210.595));
            await player.SetVisibleAsync(false);
            player.EmitLocked("guiReady");
            //   player.ShowNotification(1, "Test", 5000, "red", "AFD");
            await Task.Delay(100);



            string json = "{\"rank\": 1, \"name\": \"" + dbPlayer.firstname + "_" + dbPlayer.lastname + "\"}";

            player.EmitLocked("openWindow", "Login", json);

            Console.WriteLine("Logged in " + player.Name);
        }
        public const float FacingAngle = -180.0f;

        [AsyncClientEvent("LoginHandler:trylogin")]
        public async Task trylogin(DBPlayer player, string password)
        {


            using MySQLHandler db = new MySQLHandler();
            var dbPlayer = db.PlayerCharacter.ToList().FirstOrDefault(account => (account.socialclub == player.SocialClubId));
            if (dbPlayer == null)
            {
                Console.WriteLine("Fehler! Diesen Benutzer gibt es nicht! | " + player.SocialClubId);
                return;
            }

            lock (player)
            {
                if (!player.Exists)
                {
                    return;
                }
            }

            if (dbPlayer.password == password)
            {
                player.EmitLocked("status", "successfully");
                player.EmitLocked("closeWindow");
                player.EmitLocked("backtonormal");

                await player.SetVisibleAsync(true);
                

                if (dbPlayer.character == "[]")
                {
                    string json = "{\"customization\": [], \"level\": \"0\"}";

                    player.EmitLocked("openWindow", "CharacterCreator", json);
                    player.EmitLocked("startCamXD");
                    await player.SetPositionAsync(new Position((float)402.8664, (float)-996.4108, (float)-99.00027));
                    await player.SetRotationAsync(new Rotation(0, 0, FacingAngle));
                } else
                {
                    player.EmitLocked("setPedStuff", dbPlayer.gender, dbPlayer.character);
                    player.EmitLocked("hudready");
                    await SpawnHandler.spawnPlayer(player);
                    player.EmitLocked("closeWindow", "Inventory");


                }


            }
            else
            {
                player.EmitLocked("status", "Passwort falsch eingegeben!");

            }

        }
    }
}
