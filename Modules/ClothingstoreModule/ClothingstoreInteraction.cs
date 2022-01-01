using AltV.Net;
using AltV.Net.Async;
using AltV.Net.Interactions;
using GVRPALTV.DatenbankHandling;
using GVRPALTV.Modules.NativeMenu;
using GVRPALTV.PlayerHandling;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static GVRPALTV.Modules.NativeMenu.DialogMigrator;

namespace GVRPALTV.Modules.Clothingstoremodule
{
    class ClothingstoreInteraction : IScript
    {
        [AsyncClientEvent("Pressed_E")]
        public async Task Pressed_EFINAL(DBPlayer player)
        {
            if (!player.loggedin) return;
            var interactions = await AltInteractions.FindInteractions(player.Position, 0);

            foreach (var interaction in interactions)
            {
                if (interaction.Type == 12)
                {

                    List<Item> Items = new List<Item>();
                    Items.Add(new Item(1, "Oberbekleidung", "Passe die Kleidung deines Charakters an!", "clothingstore:cat", "torso", false, 0, false, false));
                    Items.Add(new Item(2, "T-Shirt", "Passe das T-Shirt deines Charakters an!", "clothingstore:cat", "tshirt", false, 0, false, false));
                    Items.Add(new Item(3, "Hosen", "Passe die Hose deines Charakters an!", "clothingstore:cat", "hose", false, 0, false, false));
                    Items.Add(new Item(4, "Schuhe", "Passe die Schuhe deines Charakters an!", "clothingstore:cat", "schuhe", false, 0, false, false));
                    Items.Add(new Item(5, "Hauptkleidung anziehen", "", "clothingstore:haupt", "", false, 0, false, true));
                    Items.Add(new Item(6, "Nebenkleidung anziehen", "", "clothingstore:nebenxd", "", false, 0, false, true));




                    DialogMigrator.CreateMenu(player, 44, "Kleidungsladen", "", Items);

                    DialogMigrator.ShowMenu(player, 44);

                    //  player.EmitLocked("showKleidungsladenCam");
                }
            }
        }

        public static async Task SetClothes(DBPlayer player)
        {
            var cdata = JsonConvert.DeserializeObject<ClothingModule.ClothingModule>(player.clothes);

            player.SetClothes((byte)1, (ushort)cdata.Mask, 0, 0);
            player.SetClothes((byte)3, (ushort)cdata.Koerper, 0, 0);
            player.SetClothes((byte)4, (ushort)cdata.Hose, (byte)cdata.HoseColor, 0);
            player.SetClothes((byte)6, (ushort)cdata.Schuhe, (byte)cdata.SchuheColor, 0);
            player.SetClothes((byte)8, (ushort)cdata.TShirt, (byte)cdata.TShirtColor, 0);
            player.SetClothes((byte)11, (ushort)cdata.Torso, (byte)cdata.TorsoColor, 0);
        }
        public static async Task SetNebenClothes(DBPlayer player)
        {
            var cdata = JsonConvert.DeserializeObject<ClothingModule.ClothingModule>(player.restclothes);

            player.SetClothes((byte)1, (ushort)cdata.Mask, 0, 0);
            player.SetClothes((byte)3, (ushort)cdata.Koerper, 0, 0);
            player.SetClothes((byte)4, (ushort)cdata.Hose, (byte)cdata.HoseColor, 0);
            player.SetClothes((byte)6, (ushort)cdata.Schuhe, (byte)cdata.SchuheColor, 0);
            player.SetClothes((byte)8, (ushort)cdata.TShirt, (byte)cdata.TShirtColor, 0);
            player.SetClothes((byte)11, (ushort)cdata.Torso, (byte)cdata.TorsoColor, 0);
        }
        [AsyncClientEvent("action")]
        public async Task LoadCat(DBPlayer player, string callback, string data)
        {
            if (!player.loggedin) return;
            if (callback == "clothingstore:haupt")
            {
                await SetClothes(player);
            }
            if (callback == "clothingstore:nebenxd")
            {
                await SetNebenClothes(player);
            }
            if (callback == "clothingstore:select")
            {
                if (data == "torso")
                {
                    DialogMigrator.CloseUserMenu(player, 55);
                    var cdata = JsonConvert.DeserializeObject<ClothingModule.ClothingModule>(player.clothes);
                    cdata.Torso = player.GetClothes(11).Drawable;
                    cdata.TorsoColor = player.GetClothes(11).Texture;
                    var final = JsonConvert.SerializeObject(cdata);
                    player.clothes = final;
                }
                if (data == "tshirt")
                {
                    DialogMigrator.CloseUserMenu(player, 55);
                    var cdata = JsonConvert.DeserializeObject<ClothingModule.ClothingModule>(player.clothes);
                    cdata.TShirt = player.GetClothes(8).Drawable;
                    cdata.TShirtColor = player.GetClothes(8).Texture;
                    var final = JsonConvert.SerializeObject(cdata);
                    player.clothes = final;
                }
                if (data == "hose")
                {
                    DialogMigrator.CloseUserMenu(player, 55);
                    var cdata = JsonConvert.DeserializeObject<ClothingModule.ClothingModule>(player.clothes);
                    cdata.Hose = player.GetClothes(4).Drawable;
                    cdata.HoseColor = player.GetClothes(4).Texture;
                    var final = JsonConvert.SerializeObject(cdata);
                    player.clothes = final;
                }
                if (data == "schuhe")
                {
                    DialogMigrator.CloseUserMenu(player, 55);
                    var cdata = JsonConvert.DeserializeObject<ClothingModule.ClothingModule>(player.clothes);
                    cdata.Schuhe = player.GetClothes(6).Drawable;
                    cdata.SchuheColor = player.GetClothes(6).Texture;
                    var final = JsonConvert.SerializeObject(cdata);
                    player.clothes = final;
                }
            }
            if (callback == "clothingstore:neben")
            {
                if (data == "torso")
                {
                    DialogMigrator.CloseUserMenu(player, 55);
                    var cdata2 = JsonConvert.DeserializeObject<ClothingModule.ClothingModule>(player.clothes);
                    cdata2.Torso = player.GetClothes(11).Drawable;
                    cdata2.TorsoColor = player.GetClothes(11).Texture;
                    var final = JsonConvert.SerializeObject(cdata2);
                    player.restclothes = final;
                }
                if (data == "tshirt")
                {
                    DialogMigrator.CloseUserMenu(player, 55);
                    var cdata2 = JsonConvert.DeserializeObject<ClothingModule.ClothingModule>(player.clothes);
                    cdata2.TShirt = player.GetClothes(8).Drawable;
                    cdata2.TShirtColor = player.GetClothes(8).Texture;
                    var final = JsonConvert.SerializeObject(cdata2);
                    player.restclothes = final;
                }
                if (data == "hose")
                {
                    DialogMigrator.CloseUserMenu(player, 55);
                    var cdata2 = JsonConvert.DeserializeObject<ClothingModule.ClothingModule>(player.clothes);
                    cdata2.Hose = player.GetClothes(4).Drawable;
                    cdata2.HoseColor = player.GetClothes(4).Texture;
                    var final = JsonConvert.SerializeObject(cdata2);
                    player.restclothes = final;
                }
                if (data == "schuhe")
                {
                    DialogMigrator.CloseUserMenu(player, 55);
                    var cdata2 = JsonConvert.DeserializeObject<ClothingModule.ClothingModule>(player.clothes);
                    cdata2.Schuhe = player.GetClothes(6).Drawable;
                    cdata2.SchuheColor = player.GetClothes(6).Texture;
                    var final = JsonConvert.SerializeObject(cdata2);
                    player.restclothes = final;
                }

                await SetClothes(player);
            }
            if (callback == "clothingstore:cat")
            {
                List<Item> Items = new List<Item>();
                Items.Add(new Item(1, "Auswahl", "Passe den Schnitt deiner Kleidung an!", "clothingstore:SS", data, true, 0, false, true));
                Items.Add(new Item(2, "Textur Auswahl", "Passe die Textur deiner Kleidung an!", "clothingstore:SS", "textur", true, 0, false, true));
                Items.Add(new Item(3, "Als Hauptkleidung speichern", "Speichere deine Kleidung!", "clothingstore:select", data, false, 0, true, false));
                Items.Add(new Item(4, "Als Nebenkleidung speichern", "Speichere deine Kleidung!", "clothingstore:neben", data, false, 0, true, false));




                DialogMigrator.CreateMenu(player, 55, "Kleidungsladen", "", Items);

                DialogMigrator.ShowMenu(player, 55);

                if (data == "torso")
                {
                    player.EmitLocked("showKleidungsladenCam", 0.4);

                    player.clothingzahl = 11;
                }
                else if (data == "tshirt")
                {
                    player.EmitLocked("showKleidungsladenCam", 0.4);

                    player.clothingzahl = 8;
                }
                else if (data == "hose")
                {
                    player.EmitLocked("showKleidungsladenCam", -0.3);

                    player.clothingzahl = 4;
                }
                else if (data == "schuhe")
                {
                    player.EmitLocked("showKleidungsladenCam", -0.5);

                    player.clothingzahl = 6;

                }
                }
        }
        [AsyncClientEvent("changeCountxd")]
        public async Task changeCountxd(DBPlayer player, string data, int count)
        {
            if (!player.loggedin) return;
            var color = player.GetClothes((byte)player.clothingzahl);

            if (data == "textur")
            {

                await player.SetClothesAsync((byte)player.clothingzahl, (ushort)color.Drawable, (byte)count, 0);
            } else
            {
                await player.SetClothesAsync((byte)player.clothingzahl, (ushort)count, color.Texture, 0);

            }


        }
    }
}
