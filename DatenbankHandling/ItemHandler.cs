using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GVRPALTV.DatenbankHandling
{
    public partial class ItemHandler
    {
        public int id { get; set; }

        public int itemId { get; set; }
        public string name { get; set; }

        public string label { get; set; }

        public string callback { get; set; }

        public int limit { get; set; }

        public DateTime? ablaufdatum { get; set; }

        public int price { get; set; }

        public bool legal { get; set; }
        public bool kleidung { get; set; }

        public bool abnehmbar { get; set; }




    }
}
