using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GVRPALTV.DatenbankHandling
{
    public partial class PlayerCharacter
    {
        public int id { get; set; }
        public string name { get; set; }

        public int adminlevel { get; set; }
        public string password { get; set; }
        public ulong socialclub { get; set; }

        public int forumid { get; set; }
        public long hwid { get; set; }
        public string ip { get; set; }

        public int rang { get; set; }
        public bool shield { get; set; }
        public bool ban { get; set; }
        public DateTime? bandate { get; set; }
        public DateTime? bantime { get; set; }
        public string firstname { get; set; }
        public string lastname { get; set; }
        public int money { get; set; }
        public int bank { get; set; }
        public int blackmoney { get; set; }
        public int visum { get; set; }
        public int playhours { get; set; }
        public string birthday { get; set; }
        public int health { get; set; }

        public int armor { get; set; }
        public string inventory { get; set; }

        public int gender { get; set; }
        public string character { get; set; }
        public bool bewusstlos { get; set; }
        public int deathtime { get; set; }
        public int hunger { get; set; }
        public int durst { get; set; }
        public string adresse { get; set; }
        public bool jail { get; set; }
        public int jailtime { get; set; }

        public int klogang { get; set; }
        public DateTime? lastlogin { get; set; }

        public string phone { get; set; }
        public string fraktion { get; set; }
        public string fraktion_rank { get; set; }
        public float pos_X { get; set; }
        public float pos_Y { get; set; }
        public float pos_Z { get; set; }

        public string clothes { get; set; }

        public string restclothes { get; set; }

    }
}
