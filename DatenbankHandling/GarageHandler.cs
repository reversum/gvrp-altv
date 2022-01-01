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
    public partial class GarageHandler
    {
        public int id { get; set; }
        public string name { get; set; }
        public float pedpos_x { get; set; }
        public float pedpos_y { get; set; }
        public float pedpos_z { get; set; }
        public float pedheading { get; set; }
        public float spawnpos_x { get; set; }
        public float spawnpos_y { get; set; }
        public float spawnpos_z { get; set; }
        public float spawnheading { get; set; }
        public bool blip { get; set; }
        public bool marker { get; set; }
        public string fraktion { get; set; }

        public static void LoadAllGarages()
        {

            using MySQLHandler db = new MySQLHandler();
            foreach (var garage in db.GarageHandler)
            {

                if (garage.blip)
                {
                    BlipStreamer.CreateStaticBlip(
                        garage.name + " Garage",
                        4,
                        (float)1,
                        true,
                        473,
                        new Vector3(
                            garage.spawnpos_x,
                            garage.spawnpos_y,
                            garage.spawnpos_z
                        ),
                        0);
                }
                if (garage.marker)
                {

                    var marker = MarkerManager.MarkerStreamer.Create(1,
        new Vector3(garage.pedpos_x, garage.pedpos_y, garage.pedpos_z),
        new Vector3(1, 1, 1), null, null,
        new Rgba((byte)255, (byte)136, (byte)0, (byte)100), 0, false);

                }
                AltInteractions.AddInteraction(new Interaction(11, (ulong)garage.id, new Vector3(garage.pedpos_x, garage.pedpos_y, garage.pedpos_z), 0, 2)); 

            }

        }
    }

}
