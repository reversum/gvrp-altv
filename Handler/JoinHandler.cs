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
using System.Security.Cryptography;
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
            
            var dbPlayer = db.PlayerCharacter.ToList().FirstOrDefault(account => (account.name == player.Name));
            if (dbPlayer == null)
            {
                Console.WriteLine("Fehler! Diesen Benutzer gibt es nicht! | " + player.Name);
                lock (player)
                {
                     player.Kick("Dein Account wurde nicht gefunden! Namen in den AltV Settings geändert?");
                }
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
            var dbPlayer = db.PlayerCharacter.ToList().FirstOrDefault(account => (account.name == player.Name));
            if (dbPlayer == null)
            {
                Console.WriteLine("Fehler! Diesen Benutzer gibt es nicht! | " + player.Name);
                return;
            }

            lock (player)
            {
                if (!player.Exists)
                {
                    return;
                }
            }
            var pass2 = dbPlayer.password;
            var pass = ComputeSha256Hash(password);


                if (dbPlayer.password == pass)
            {
                player.socialclub = player.SocialClubId;
                player.EmitLocked("status", "successfully");
                player.EmitLocked("closeWindow");
                player.EmitLocked("backtonormal");//SS

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
            static string ComputeSha256Hash(string rawData)
            {
                // Create a SHA256   
                using (SHA256 sha256Hash = SHA256.Create())
                {
                    // ComputeHash - returns byte array  
                    byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(rawData));

                    // Convert byte array to a string   
                    StringBuilder builder = new StringBuilder();
                    for (int i = 0; i < bytes.Length; i++)
                    {
                        builder.Append(bytes[i].ToString("x2"));
                    }
                    return builder.ToString();
                }
            }
        }
}
