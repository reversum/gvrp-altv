using System;
using AltV.Net;
using AltV.Net.Elements.Entities;
using GVRPALTV.ColshapeHandling;
using GVRPALTV.PlayerHandling;
using GVRPALTV.VehicleHandling;

namespace GVRPALTV.EntitySync
{
    public class VehicleFactory : IEntityFactory<IVehicle>
    {
        public IVehicle Create(IServer server, IntPtr entityPointer, ushort id)
        {
            return new DBVehicle(server, entityPointer, id);
        }
    }

    public class CustomPlayerFactory : IEntityFactory<IPlayer>
    {
        public IPlayer Create(IServer server, IntPtr entityPointer, ushort id)
        {
            return new DBPlayer(server, entityPointer, id);
        }
    }

    public class ColshapeFactory : IBaseObjectFactory<IColShape>
    {
        public IColShape Create(IServer server, IntPtr entityPointer)
        {
            return new CustomColshape(server, entityPointer);
        }
    }
}