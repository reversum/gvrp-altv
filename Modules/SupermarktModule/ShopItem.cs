using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GVRPALTV.Modules.SupermarktModule
{
    class ShopItem
    {
        public int id { get; set; }

        public string name { get; set; }
        public int price { get; set; }
        public int endprice { get; set; }
        public int count { get; set; }



        public ShopItem(int ide, string namee, int pricee, int endpricee, int counte)
        {
            id = ide;
            name = namee;
            price = pricee;
            endprice = endpricee;
            count = counte;
        }
    }
}
