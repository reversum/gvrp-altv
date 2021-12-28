using AltV.Net;
using AltV.Net.Async;
using GVRPALTV.PlayerHandling;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static GVRPALTV.Modules.InventoryModule.InventorySettings;

namespace GVRPALTV.Modules.InventoryModule
{
    public class Inventory : IScript
    {

        [AsyncClientEvent("openInventory")]
        public async Task openInventory(DBPlayer player)
        {

            if (player.inventory == "{\"Name\": \"Inventar\"}")
            {
                var inventory = JsonConvert.DeserializeObject<InventorySettings>(player.inventory);

            //    var slots = JsonConvert.DeserializeObject<Items>(inventory.Slots);

                inventory.Id = 1;
                inventory.Name = "Inventar";
                inventory.Money = player.money;
                inventory.MaxWeight = 4000;
                inventory.MaxSlots = 15;


                var finishinventory = JsonConvert.SerializeObject(inventory);
                player.EmitLocked("openWindow", "Inventory", "{\"inventory\":[{\"Name\":\"Inventar\", \"MaxWeight\":4000,\"MaxSlots\":15,\"Id\":1,\"Money\":0,\"Slots\":[]}]}");



            }
            else
            {

              
                player.EmitLocked("openWindow", "Inventory", "{\"inventory\":[" + player.inventory +"]}");

            }






        }
    }
}
