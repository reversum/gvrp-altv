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
    public partial class KleidungsladenHandler
    {
        public int id { get; set; }
        public string name { get; set; }
        public float pedpos_x { get; set; }
        public float pedpos_y { get; set; }
        public float pedpos_z { get; set; }
        public float pedheading { get; set; }
        public bool blip { get; set; }
        public bool marker { get; set; }

        public static void LoadAllClothingstores()
        {

            using MySQLHandler db = new MySQLHandler();
            foreach (var clothingstore in db.KleidungsladenHandler)
            {

                if (clothingstore.blip)
                {
                    BlipStreamer.CreateStaticBlip(
                        clothingstore.name + " Kleidungsladen",
                        0,
                        (float)1,
                        true,
                        73,
                        new Vector3(
                            clothingstore.pedpos_x,
                            clothingstore.pedpos_y,
                            clothingstore.pedpos_z
                        ),
                        0);
                }
                if (clothingstore.marker)
                {

                    var marker = MarkerManager.MarkerStreamer.Create(1,
        new Vector3(clothingstore.pedpos_x, clothingstore.pedpos_y, clothingstore.pedpos_z),
        new Vector3(1, 1, 1), null, null,
        new Rgba((byte)255, (byte)136, (byte)0, (byte)100), 0, false);

                }
                AltInteractions.AddInteraction(new Interaction(12, (ulong)clothingstore.id, new Vector3(clothingstore.pedpos_x, clothingstore.pedpos_y, clothingstore.pedpos_z), 0, 4)); 

            }

        }
    }

}
