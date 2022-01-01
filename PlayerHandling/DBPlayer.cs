using AltV.Net;
using AltV.Net.Data;
using AltV.Net.Elements.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AltV.Net.Async;
using AltV.Net.Data;
using AltV.Net.Elements.Entities;
using System.Threading.Tasks;
using GVRPALTV.EntitySync;
using GVRPALTV.Modules.InventoryModule;
using Newtonsoft.Json;
using GVRPALTV.DatenbankHandling;

namespace GVRPALTV.PlayerHandling
{
    public class DBPlayer : Player
    {

        public int accountid { get; set; } = 0;
        public int forumid { get; set; } = 0;
        public bool loggedin { get; set; } = false;
        public string name { get; set; }

        public int adminlevel { get; set; }
        public string password { get; set; }
        public ulong socialclub { get; set; }
        public long hwid { get; set; }
        public string ip { get; set; }

        public int rang { get; set; }
        public bool shield { get; set; }
        public bool ban { get; set; }
        public DateTime? bandate { get; set; }
        public DateTime? bantime { get; set; }
        public string firstname { get; set; }
        public string lastname { get; set; }
        public int money { get; set; }
        public int bank { get; set; }
        public int blackmoney { get; set; }
        public int visum { get; set; }
        public int playhours { get; set; }
        public string birthday { get; set; }
        public int health { get; set; }

        public int armor { get; set; }
        public string inventory { get; set; }

        public int gender { get; set; }
        public string character { get; set; }
        public bool bewusstlos { get; set; }
        public int deathtime { get; set; }
        public int hunger { get; set; }
        public int durst { get; set; }
        public string adresse { get; set; }
        public bool jail { get; set; }
        public int jailtime { get; set; }

        public int klogang { get; set; }
        public DateTime? lastlogin { get; set; }

        public string phone { get; set; }
        public string fraktion { get; set; }
        public string fraktion_rank { get; set; }
        public float pos_X { get; set; }
        public float pos_Y { get; set; }
        public float pos_Z { get; set; }

        public string currentminijob { get; set; }

        public bool minijobactive { get; set; }
        public int minijobcount { get; set; }
        public int minijobmax { get; set; }

        public IBlip minijobblip { get; set; }
        public MarkerManager.Marker minijobmarker { get; set; }


        public int clothingzahl { get; set; }
        public uint WatchMenu { get; set; }
        public string clothes { get; set; }
        public string restclothes { get; set; }

        public DBPlayer(IServer server, IntPtr nativePointer, ushort id) : base(server, nativePointer, id)
        {
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="type">1-7</param>
        /// <param name="message"></param>
        /// <param name="duration">in milliseconds</param>
        /// 

        public async Task RemoveInventoryItem(int id, int count)
        {
            using MySQLHandler db = new MySQLHandler();
            foreach (var items in db.ItemHandler)
            {
                if (items.itemId == id)
                {
                    var inventory = JsonConvert.DeserializeObject<InventorySettings>(this.inventory);

                    inventory.Slots.Remove(inventory.Slots.ToList().FirstOrDefault(item => (item.Id == id)));


                }
            }
        }
                    public async Task AddInventoryItem( int id, int count)
        {
            using MySQLHandler db = new MySQLHandler();
            foreach (var items in db.ItemHandler)
            {
                if (items.itemId == id)
                {

                    var inventory = JsonConvert.DeserializeObject<InventorySettings>(this.inventory);

                    var dbItem = inventory.Slots.ToList().FirstOrDefault(item => (item.Id == id));

                    if (dbItem != null)
                    {

                        dbItem.quantity = dbItem.quantity + count;
                        inventory.Slots.Add(new InventorySettings.Items(items.label, items.name, dbItem.quantity, items.limit, 0, items.id, 4, ""));

                        inventory.Slots.Remove(inventory.Slots.ToList().FirstOrDefault(item => (item.Id == id)));


                    }
                    else
                    {


                        inventory.Slots.Add(new InventorySettings.Items(items.label, items.name, count, items.limit, 0, items.id, 4, ""));

                    }

                    var finishinventory = JsonConvert.SerializeObject(inventory);

                    this.inventory = finishinventory;
                    break;
                }
            }
        }
        public async Task ShowNotification(string message)
        {
            if (!Exists)
            {
                Console.WriteLine("Early Return");
                return;
            }
            try
            {
                await this.EmitAsync("sendPlayerNotification", message, 1, 1, 1);
            }
            catch (Exception e)
            {
                Console.WriteLine($"{e}");
            }
        }
        public async Task ShowAdvancedNotification(string message, int iconType, string title = "Title", string subtitle = "Hilfe", string notifImage = "char_antonia", string backgroundColor = null, int durationMult = 1)
        {
            if (!Exists)
            {
                Console.WriteLine("Early Return");
                return;
            }
            try
            {
                await this.EmitAsync("advancednotify", message, iconType, title, subtitle, notifImage, backgroundColor, durationMult);
            }
            catch (Exception e)
            {
                Console.WriteLine($"{e}");
            }
        }

        


    }
}
