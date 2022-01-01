using AltV.Net;
using AltV.Net.Async;
using AltV.Net.Interactions;
using GVRPALTV.DatenbankHandling;
using GVRPALTV.Modules.NativeMenu;
using GVRPALTV.Modules.SupermarktModule;
using GVRPALTV.PlayerHandling;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static GVRPALTV.Modules.NativeMenu.DialogMigrator;

namespace GVRPALTV.Modules.Supermarketmodule
{
    class Supermarketmodule : IScript
    {
        [AsyncClientEvent("Pressed_E")]
        public async Task Pressed_EFINAL(DBPlayer player)
        {
            if (!player.loggedin) return;
            var interactions = await AltInteractions.FindInteractions(player.Position, 0);

            foreach (var interaction in interactions)
            {
                if (interaction.Type == 13)
                {
                    List<ShopItem> Items = new List<ShopItem>();
                    Items.Add(new ShopItem(12, "Schwamm", 12, 0, 1));

                    Shop shop = new Shop(1, "Supermarkt", Items);


                    var finishinventory = JsonConvert.SerializeObject(shop);
                    player.EmitLocked("openWindow", "Shop", finishinventory);


                }
            }
        }

        [AsyncClientEvent("Shop:BuyProduct")]
        public async Task BuyProduct(DBPlayer player, string product)
        {
            var cdata = JsonConvert.DeserializeObject<ShopItem>(product);

            if (player.money > cdata.endprice)
            {
                player.money = player.money - cdata.endprice;
                await player.AddInventoryItem(cdata.id, cdata.count);


            } else
            {
                player.ShowNotification("Du hast nicht genug ~r~Geld");
                await player.AddInventoryItem(cdata.id, cdata.count);

            }

        }
    }
}
