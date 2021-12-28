using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GVRPALTV.JSON
{
    public class CharacterParts
    {

            public class Characterization
            {
            public string name;
            public string method;
            public List<CharacterizationSettings> settings;

        }

        public class CharacterizationSettings
        {
            public string name;
            public int price;
            public uint value;
            public int min;
            public int max;
            public int step;
            public int normal;
        }

            public List<Characterization> characterizations;
        
    }
}
