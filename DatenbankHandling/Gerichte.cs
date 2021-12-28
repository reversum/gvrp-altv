using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GVRPALTV.DatenbankHandling
{
    public partial class Gerichte
    {
        public int id { get; set; }
        public string name { get; set; }
        public string zutaten { get; set; }
        public string zugelassenegerate { get; set; }

        public int health { get; set; }
        public int hunger { get; set; }
        public int durst { get; set; }
        public bool durchfall { get; set; }
        public DateTime? ablaufdatum { get; set; }
        public int klogang { get; set; }
    }
}
