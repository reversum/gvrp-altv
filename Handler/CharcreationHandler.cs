using AltV.Net;
using AltV.Net.Async;
using AltV.Net.Data;
using AltV.Net.Enums;
using GVRPALTV.DatenbankHandling;
using GVRPALTV.JSON;
using GVRPALTV.PlayerHandling;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GVRPALTV.Handler
{
    class CharcreationHandler : IScript
    {

        [AsyncClientEvent("rotate:char")]
        public async Task rotate(DBPlayer player, float rotate)
        {
            await player.SetRotationAsync(new Rotation(player.Rotation.Roll, player.Rotation.Pitch, player.Rotation.Yaw + rotate));
        }
        [AsyncClientEvent("changeCharacterPartFFeature")]
        public async Task changeCharacterPartFFeature(DBPlayer player, string method, uint eins, uint zwei)
        {
   
            if (method == "setFaceFeature")
            {
                player.SetFaceFeature((byte)eins, zwei);
            }
      
        }
        [AsyncClientEvent("changeCharacterPartHOver")]
        public async Task changeCharacterPartHOver(DBPlayer player, string method, uint eins, uint zwei, uint drei, uint vier, uint funf)
        {

            if (method == "setHeadOverlay")
            {
                player.SetHeadOverlay((byte)eins, (byte)zwei, (float)drei);
                player.SetHeadOverlayColor((byte)vier, (byte)funf, 0, 0);
            }

        }
        [AsyncClientEvent("changeCharacterPartHead")]
        public async Task changeCharacterPart(DBPlayer player, string method, uint eins, uint zwei, uint drei, uint vier, uint fuenf, uint sechs, uint sieben, uint acht, uint neun)
        {

            if (method == "setHeadBlendData")
            {
                player.SetHeadBlendData(eins, zwei, drei, vier, fuenf, sechs, sieben, acht, neun);
            }
            
        }
        [AsyncClientEvent("changeCharacterPartEye")]
        public async Task changeCharacterPartEye(DBPlayer player, string method, uint eins)
        {
            if (method == "setEyeColor")
            {
                player.SetEyeColor((ushort)eins);
            }

        }
        [AsyncClientEvent("changeCharacterPartFace")]
        public async Task changeCharacterPartFace(DBPlayer player, string method, uint eins, uint zwei)
        {
            if (method == "setFaceFeature")
            {
                player.SetFaceFeature((byte)eins, zwei);
            }

        }
        [AsyncClientEvent("changeCharacterPartComp")]
        public async Task changeCharacterPartComp(DBPlayer player, string method, uint eins, uint zwei, uint drei, uint vier)
        {
            if (method == "setComponentVariation")
            {
                player.SetClothes((byte)eins, (ushort)zwei, (byte)drei, (byte)vier);
            }
  
        }




        
        [AsyncClientEvent("changeGender")]
        public async Task changeGender(DBPlayer player, bool male)
        {
            using MySQLHandler db = new MySQLHandler();

            var dbPlayer = db.PlayerCharacter.ToList().FirstOrDefault(account => (account.socialclub == player.SocialClubId));
            if (dbPlayer == null)
            {
                return;
            }
            Console.WriteLine(male);
            if (player.Model == (uint)PedModel.FreemodeFemale01)
            {
                await player.SetModelAsync((uint)PedModel.FreemodeMale01);
                dbPlayer.gender = 0;
            }
            else
            {
                await player.SetModelAsync((uint)PedModel.FreemodeFemale01);
                dbPlayer.gender = 1;

            }


        }
        [AsyncClientEvent("UpdateCharacterCustomization")]
        public async Task UpdateCharacterCustomization(DBPlayer player, string data, int price)
        {
            player.character = data;

            Console.WriteLine("Charakter geupdatet: " + player.firstname + "_ " + player.lastname + " | DATA: " + data);
            using MySQLHandler db = new MySQLHandler();

            var dbPlayer = db.PlayerCharacter.ToList().FirstOrDefault(account => (account.socialclub == player.SocialClubId));
            if (dbPlayer != null)
            {
                dbPlayer.character = player.character;
                await db.SaveChangesAsync();
            }


        }
    }
}
