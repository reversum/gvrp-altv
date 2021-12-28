using System;
using AltV.Net;
using AltV.Net.Data;
using AltV.Net.Elements.Entities;
using GVRPALTV.PlayerHandling;

namespace GVRPALTV.ColshapeHandling
{
    public class CustomColshape : ColShape
    {
        public int ColshapeId { get; set; } = 0;
        public string ColshapeName { get; set; } = "None";
        public string CarDealerVehName { get; set; }
        public ulong CarDealerVehPrice { get; set; }
        public float Radius { get; set; }


        public CustomColshape(IServer server, IntPtr nativePointer) : base(server, nativePointer) { }

        public bool IsInRange(DBPlayer player)
        {
            Position position = Position.Zero;
            lock (this)
            {
                if (this.Exists)
                {
                    position = this.Position;
                }
                else
                {
                    return false;
                }
            }
            lock (player)
            {
                if (!player.Exists) return false;

                return player.Position.Distance(position) <= Radius;
            }
        }
    }
}