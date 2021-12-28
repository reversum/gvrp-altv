using GVRPALTV.EntitySync;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace GVRPALTV.DatenbankHandling
{
    public partial class BlipHandler
    {
        public int id { get; set; }
        public string name { get; set; }
        public int color { get; set; }
        public float scale { get; set; }
        public bool shortrange { get; set; }
        public int sprite { get; set; }
        public float pos_x { get; set; }
        public float pos_y { get; set; }
        public float pos_z { get; set; }

        [Obsolete]
        public static string GetAllServerBlipsInJson()
        {
            using MySQLHandler db = new MySQLHandler();
            var blipList = db.BlipHandler.ToList().Select(blip => new
            {
                name = blip.name,
                color = blip.color,
                scale = blip.scale,
                posX = blip.pos_x,
                posY = blip.pos_y,
                posZ = blip.pos_z,
                shortRange = blip.shortrange,
                sprite = blip.sprite
            }).ToList();
            return JsonConvert.SerializeObject(blipList);
        }

        /// <summary>
        /// https://wiki.altv.mp/wiki/GTA:Blips
        /// </summary>
        /// <returns></returns>
        public static void LoadAllBlipsFromDb()
        {
     
            int count = 0;
            using MySQLHandler db = new MySQLHandler();
            foreach (var blip in db.BlipHandler)
            {

                BlipStreamer.CreateStaticBlip(
                    blip.name,
                    blip.color,
                    (float)blip.scale,
                    blip.shortrange,
                    blip.sprite,
                    new Vector3(
                        blip.pos_x,
                        blip.pos_y,
                        blip.pos_z
                    ),
                    0);
                count++;
            }
  
        }
    }

    
}
