using AltV.Net.Data;
using AltV.Net.Interactions;
using GVRPALTV.EntitySync;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace GVRPALTV.DatenbankHandling
{
    public partial class SupermarketHandler
    {
        public int id { get; set; }
        public string name { get; set; }
        public float pedpos_x { get; set; }
        public float pedpos_y { get; set; }
        public float pedpos_z { get; set; }
        public float pedheading { get; set; }
        public bool blip { get; set; }
        public bool marker { get; set; }

        public static void LoadAllSupermarket()
        {

            using MySQLHandler db = new MySQLHandler();
            foreach (var supermarkt in db.SupermarketHandler)
            {

                if (supermarkt.blip)
                {
                    BlipStreamer.CreateStaticBlip(
                        supermarkt.name + " Supermarkt",
                        73,
                        (float)1,
                        true,
                        52,
                        new Vector3(
                            supermarkt.pedpos_x,
                            supermarkt.pedpos_y,
                            supermarkt.pedpos_z
                        ),
                        0);
                }
                if (supermarkt.marker)
                {

                    var marker = MarkerManager.MarkerStreamer.Create(1,
        new Vector3(supermarkt.pedpos_x, supermarkt.pedpos_y, supermarkt.pedpos_z),
        new Vector3(1, 1, 1), null, null,
        new Rgba((byte)255, (byte)136, (byte)0, (byte)100), 0, false);

                }
                AltInteractions.AddInteraction(new Interaction(13, (ulong)supermarkt.id, new Vector3(supermarkt.pedpos_x, supermarkt.pedpos_y, supermarkt.pedpos_z), 0, 4)); 

            }

        }
    }

}
