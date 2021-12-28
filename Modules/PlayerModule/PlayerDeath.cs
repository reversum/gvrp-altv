using AltV.Net;
using AltV.Net.Async;
using AltV.Net.Elements.Entities;
using GVRPALTV.Modules.DeathModule;
using GVRPALTV.PlayerHandling;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GVRPALTV.Modules.PlayerModule
{
    class PlayerDeath : IScript
    {
        [AsyncScriptEvent(ScriptEventType.PlayerDead)]
        public async Task Tot(DBPlayer player, IEntity attacker, uint weapon)
        {
            if(player.bewusstlos) { return; }
            KillPlayer.killPlayer(player);
        }
    }
}