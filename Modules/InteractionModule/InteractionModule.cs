using AltV.Net;
using AltV.Net.Async;
using AltV.Net.Interactions;
using GVRPALTV.DatenbankHandling;
using GVRPALTV.Modules.NativeMenu;
using GVRPALTV.PlayerHandling;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static GVRPALTV.Modules.NativeMenu.DialogMigrator;

namespace GVRPALTV.Modules.InteractionModule
{
    class InteractionModule : IScript
    {
        [AsyncClientEvent("Pressed_E")]
        public async Task Pressed_EFINAL(DBPlayer player)
        {
            if (!player.loggedin) return;

            var interactions = await AltInteractions.FindInteractions(player.Position, 0);

            foreach (var interaction in interactions)
            {
                if (interaction.Type == 11)
                {
                    using MySQLHandler db = new MySQLHandler();
                    var garage = db.GarageHandler.ToList().FirstOrDefault(garages => ((ulong)garages.id == interaction.Id));

                    List<Item> Items = new List<Item>();
                    Items.Add(new Item(1, "Fahrzeug einparken", "", "garage:einparken", garage.name, false, 0, false, false));

                    int id = 2;
                    foreach (var vehicle in db.VehicleHandler)
                    {
                        if (vehicle.ownerid == player.accountid && vehicle.ingarage)
                        {
                            Items.Add(new Item(id, vehicle.plate, "", "garage:parkout", garage.name + "," + vehicle.id, false, 0, true, false));
                            id = id + 1;
                        }
                    }


                    DialogMigrator.CreateMenu(player, 44, garage.name + " Garage", "", Items);

                    DialogMigrator.ShowMenu(player, 44);
                }
            }
        }
    }
}
