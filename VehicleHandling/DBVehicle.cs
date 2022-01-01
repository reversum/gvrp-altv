
using System;
using AltV.Net;
using AltV.Net.Data;
using AltV.Net.Elements.Entities;
using GVRPALTV.DatenbankHandling;

namespace GVRPALTV.VehicleHandling
{
    public class DBVehicle : Vehicle
    {
        public int id { get; set; }
        public int ownerid { get; set; }

        public ushort entityid { get; set; }
        public string plate { get; set; }
        public string name { get; set; }
        public ulong hash { get; set; }
        public float price { get; set; }
        public string trunk { get; set; }
        public int trunkweight { get; set; }
        public int MaxFuel { get; set; }
        public float pos_X { get; set; }
        public float pos_Y { get; set; }
        public float pos_Z { get; set; }
        public float rotation { get; set; }
        public string tuning { get; set; }
        public bool engine { get; set; } 
        public bool ingarage { get; set; } 
        public int health { get; set; }
        public bool locked { get; set; }

        public bool angemeldet { get; set; }

        public DBVehicle(IServer server, IntPtr nativePointer, ushort id) : base(server, nativePointer, id)
        {
        }

        public DBVehicle(IServer server, uint model, Position position, Rotation rotation) : base(server, model, position, rotation)
        {
        }


    }
    }