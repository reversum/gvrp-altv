using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GVRPALTV.DatenbankHandling
{
    public partial class PlayerHouse
    {
        public int id { get; set; }
        public string playerid { get; set; }

        public string name { get; set; }

        public bool kueche { get; set; }

        public string moebel { get; set; }

        public string locked { get; set; }
        public int mietplaetze { get; set; }

        public int preis { get; set; }

        public string nachricht { get; set; }

        public bool tosell { get; set; }

        public string keys { get; set; }

        public string kuehlschrank { get; set; }

        public int kuehlschrankplatz { get; set; }

        public string schrank { get; set; }

        public int schrankplatz { get; set; }

        public string kleidungen { get; set; }

        public float pos_X { get; set; }
        public float pos_Y { get; set; }
        public float pos_Z { get; set; }




    }
}
