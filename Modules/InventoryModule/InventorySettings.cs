using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GVRPALTV.Modules.InventoryModule
{
    public class InventorySettings
    {
            public string Name;
            public int MaxWeight;
            public int MaxSlots;
            public int Id;
            public int Money;
            public IList<string> Slots;
        

        public class Items
        {

            public string Label;
            public string Name;
            public int quantity;
            public int MaxStackSize;
            public int Amount;
            public int Id;
            public int Slot;
            public string ImagePath;


        }




    }
}
