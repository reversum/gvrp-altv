using AltV.Net;
using AltV.Net.Async;
using GVRPALTV.PlayerHandling;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GVRPALTV.Modules.ClothingModule;

namespace GVRPALTV.Modules.NativeMenu
{

    public enum PlayerMenu : uint
    {
        Garage = 1,
    }

    public class MenuHandler : IScript
    {
        [AsyncClientEvent("closeNativeMenu")]
        public async Task closeNativeMenu(DBPlayer player, int id)
        {
            Console.WriteLine(id);
            if (id == 55)
            {
                var cdata = JsonConvert.DeserializeObject<ClothingModule.ClothingModule>(player.clothes);

                player.SetClothes((byte)1, (ushort)cdata.Mask, 0, 0);
                player.SetClothes((byte)3, (ushort)cdata.Koerper, 0, 0);
                player.SetClothes((byte)4, (ushort)cdata.Hose, (byte)cdata.HoseColor, 0);
                player.SetClothes((byte)6, (ushort)cdata.Schuhe, (byte)cdata.SchuheColor, 0);
                player.SetClothes((byte)8, (ushort)cdata.TShirt, (byte)cdata.TShirtColor, 0);
                player.SetClothes((byte)11, (ushort)cdata.Torso, (byte)cdata.TorsoColor, 0);
                player.EmitLocked("hideKleidungsladenCam");
            }
        }
    }
    public static class DialogMigrator 
    {
        public static void OpenUserMenu(DBPlayer iPlayer, uint MenuID, bool nofreeze = false)
        {
            if (iPlayer.WatchMenu > 0)
            {
                //CloseUserMenu(iPlayer.Player, iPlayer.WatchMenu);
            }

            ShowMenu(iPlayer, MenuID);
            iPlayer.WatchMenu = MenuID;
        }

        public static void CloseUserMenu(DBPlayer player, uint MenuID, bool noHide = false)
        {
            DBPlayer iPlayer = player;
            if (iPlayer == null) return;
            if (!noHide) HideMenu(player, MenuID);
            /*if (iPlayer.Freezed == false)
            {
                player.FreezePosition = false;
            }*/

            iPlayer.WatchMenu = 0;
        }

        public class Item
        {
            public int id { get; set; }

            public string Label { get; set; }

            public string Description { get; set; }
            public string Callback { get; set; }
            public string Data { get; set; }

            public bool countable { get; set; }
            public int count { get; set; }

            public bool Favorite { get; set; }
            public bool Information { get; set; }

            public Item(int ide, string label, string description, string callback, string data, bool cockable, int ccount, bool fav, bool info)
            {
                id = ide;
                Label = label;
                Description = description;
                Callback = callback;
                Data = data;
                countable = cockable;
                count = ccount;
                Favorite = fav;
                Information = info;

            }
        }

        public static List<Item> Items = new List<Item>();
        
        public static void CreateMenu(DBPlayer player, uint menuid, string name = "", string description = "", List<Item> items = null)
        {
            Console.WriteLine(JsonConvert.SerializeObject(items));

            player.Emit("componentServerEvent", "NativeMenu", "createMenu", name, description, JsonConvert.SerializeObject(items));
        }

        public static void AddMenuItem(DBPlayer player, uint menuid, string label, string description, string callback)
        {

            Console.WriteLine(JsonConvert.SerializeObject(Items));

           // player.Emit( "addItems", JsonConvert.SerializeObject(Items));
        }

        public static void ShowMenu(DBPlayer player, uint menuid)
        {
            player.Emit("componentServerEvent", "NativeMenu", "show", menuid);
        }

        private static void HideMenu(DBPlayer player, uint menuid)
        {
            player.Emit("componentServerEvent", "NativeMenu", "hide");
        }


    }
}
