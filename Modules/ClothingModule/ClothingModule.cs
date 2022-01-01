using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GVRPALTV.Modules.ClothingModule
{
    class ClothingModule
    {
        public int Mask { get; set; }

        public int Koerper { get; set; }
        public int Hose { get; set; }
        public int HoseColor { get; set; }

        public int Schuhe { get; set; }
        public int SchuheColor { get; set; }
        public int TShirt { get; set; }
        public int TShirtColor { get; set; }

        public int Torso { get; set; }
        public int TorsoColor { get; set; }


        public ClothingModule(int maske, int koeper, int hose, int hosecolor, int schuhe, int schuhecolor, int tshirt, int tshirtcolor, int torso, int torsocolor)
        {
            Mask = maske;
            Koerper = koeper;
            Hose = hose;
            HoseColor = hosecolor;
            Schuhe = schuhe;
            SchuheColor = schuhecolor;
            TShirt = tshirt;
            TShirtColor = tshirtcolor;
            Torso = torso;
            TorsoColor = torsocolor;

        }


       
    }
}
