using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GVRPALTV.DatenbankHandling
{
    public partial class MiniJobDeliveryHandler
    {
        public int id { get; set; }
        public string jobname { get; set; }
        public float pos_x { get; set; }
        public float pos_y { get; set; }
        public float pos_z { get; set; }
    }
}
