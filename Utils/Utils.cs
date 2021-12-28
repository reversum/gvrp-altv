using AltV.Net.Data;
using AltV.Net.Elements.Entities;
using GVRPALTV.EntitySync;
using GVRPALTV.VehicleHandling;

namespace GVRPALTV.Utils
{
    public static partial class Extensions
    {
        public static bool IsInRange(this Position currentPosition, Position otherPosition, float distance)
        {
            return currentPosition.Distance(otherPosition) <= distance;
        }

        public static ulong GetVehicleId(this IVehicle vehicle)
        {
            var myVehicle = (DBVehicle)vehicle;
            if (myVehicle == null || !myVehicle.Exists) return 0;
            return (ulong)myVehicle.id;
        }

        public static bool HasVehicleId(this IVehicle vehicle)
        {
            var myVehicle = (DBVehicle)vehicle;
            if (myVehicle == null || !myVehicle.Exists) return false;
            return myVehicle.id != 0;
        }

        public static void SetVehicleId(this IVehicle vehicle, int vehicleId)
        {
            var myVehicle = (DBVehicle)vehicle;
            if (myVehicle == null || !myVehicle.Exists) return;
            myVehicle.id = (int)vehicleId;
        }
        public static ulong GetVehicleOwner(this IVehicle vehicle)
        {
            var myVehicle = (DBVehicle)vehicle;
            if (myVehicle == null || !myVehicle.Exists) return 0;
            return (ulong)myVehicle.ownerid;
        }

        public static bool HasVehicleOwner(this IVehicle vehicle)
        {
            var myVehicle = (DBVehicle)vehicle;
            if (myVehicle == null || !myVehicle.Exists) return false;
            return myVehicle.ownerid != 0;
        }

        public static void SetVehicleOwner(this IVehicle vehicle, int vehicleId)
        {
            var myVehicle = (DBVehicle)vehicle;
            if (myVehicle == null || !myVehicle.Exists) return;
            myVehicle.ownerid = (int)vehicleId;
        }

    
}
}