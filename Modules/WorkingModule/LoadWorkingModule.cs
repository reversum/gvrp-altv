using AltV.Net;
using AltV.Net.Data;
using AltV.Net.Interactions;
using GVRPALTV.EntitySync;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace GVRPALTV.Modules.WorkingModule
{
    class LoadWorkingModule : IScript
    {
        public static void LoadAllWorkingModules()
        {
            //Position(x: -1178,6505, y: -891,6528, z: 13,744385)
            AltInteractions.AddInteraction(new Interaction(0, 31, new Vector3(-1178, -891, 13), 0, 3)); //Burgershot


        }
    }
}
