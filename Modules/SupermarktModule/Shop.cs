using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GVRPALTV.Modules.SupermarktModule
{
    class Shop
    {
        public int id { get; set; }

        public string title { get; set; }
        public List<ShopItem> items { get; set; }

        public Shop(int ide, string titlee, List<ShopItem> itemse)
        {
            id = ide;
            title = titlee;
            items = itemse;

        }
    }
}
