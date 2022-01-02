using AltV.Net;
using AltV.Net.Async;
using AltV.Net.Data;
using AltV.Net.Elements.Entities;
using AltV.Net.EntitySync;
using AltV.Net.EntitySync.ServerEvent;
using AltV.Net.EntitySync.SpatialPartitions;
using AltV.Net.Interactions;
using GVRPALTV.DatenbankHandling;
using GVRPALTV.EntitySync;
using GVRPALTV.Modules.BeduerfnisseModule;
using GVRPALTV.Modules.DeathModule;
using GVRPALTV.Modules.TimerModule;
using GVRPALTV.Modules.WorkingModule;
using GVRPALTV.PlayerHandling;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Timers;

namespace GVRPALTV
{
    internal class Main : AsyncResource
    {

        public override IEntityFactory<IPlayer> GetPlayerFactory()
        {
            return new CustomPlayerFactory();
        }

        public override IBaseObjectFactory<IColShape> GetColShapeFactory()
        {
            return new ColshapeFactory();
        }

        public override IEntityFactory<IVehicle> GetVehicleFactory()
        {
            return new VehicleFactory();
        }
        public override void OnStart()
        {
            Logger.Logger.info("Server wurde gestartet!");


            Console.WriteLine("AHH");
            AltEntitySync.Init(5, (syncrate) => 200, (threadId) => false,
    (threadCount, repository) => new ServerEventNetworkLayer(threadCount, repository),
    (entity, threadCount) => entity.Type,
    (entityId, entityType, threadCount) => entityType,
    (threadId) =>
    {
        return threadId switch
        {
            //MARKER
            0 => new LimitedGrid3(50_000, 50_000, 75, 10_000, 10_000, 64),
            //TEXT
            1 => new LimitedGrid3(50_000, 50_000, 75, 10_000, 10_000, 32),
                    //PROP
                    2 => new LimitedGrid3(50_000, 50_000, 100, 10_000, 10_000, 1500),
                    //HELP TEXT
                    3 => new LimitedGrid3(50_000, 50_000, 100, 10_000, 10_000, 1),
                    //BLIP
                    4 => new GlobalEntity(),
                    //BLIP DYNAMIC
                    5 => new LimitedGrid3(50_000, 50_000, 175, 10_000, 10_000, 200),
            _ => new LimitedGrid3(50_000, 50_000, 175, 10_000, 10_000, 115),
        };
    },
    new IdProvider());
            AltInteractions.Init();

            BlipHandler.LoadAllBlipsFromDb();
            LoadWorkingModule.LoadAllWorkingModules();
            VehicleHandler.LoadAllVehiclesFromDb();
            GarageHandler.LoadAllGarages();
            KleidungsladenHandler.LoadAllClothingstores();
            SupermarketHandler.LoadAllSupermarket();

            Timer timeVehicleTimer = new Timer();
            timeVehicleTimer.Interval = 30000;
            timeVehicleTimer.Elapsed += VehicleTimer.OnVehicleTimer;
            timeVehicleTimer.Start();
            Timer timeSyncTimer = new Timer();
            timeSyncTimer.Interval = 60000;
            timeSyncTimer.Elapsed += PlayerTimer.OnTimeSyncTimer;
            timeSyncTimer.Start();
            Timer syncTimer = new Timer();
            syncTimer.Interval = 120000;
            syncTimer.Elapsed += PlayerTimer.OnSyncTimer;
            syncTimer.Start();
            Timer eatTimer = new Timer();
            eatTimer.Interval = 90000;
            eatTimer.Elapsed += BeduerfnisseTimer.OnHunger;
            eatTimer.Start();
            Timer durstTimer = new Timer();
            durstTimer.Interval = 120000;
            durstTimer.Elapsed += BeduerfnisseTimer.OnDurst;
            durstTimer.Start();
            Timer deathTimer = new Timer();
            deathTimer.Interval = 5000;
            deathTimer.Elapsed += DeathTimer.OnDeathTimer;
            deathTimer.Start();
            Timer klogang = new Timer();
               klogang.Interval = 120000;
             //  klogang.Interval = 3000;

            klogang.Elapsed += BeduerfnisseTimer.OnKlo;
            klogang.Start();

            CreateMarkers();

        }
        private void CreateMarkers()
        {
            // Create some markers
        }

        public override void OnStop()
        {
             using MySQLHandler db = new MySQLHandler();
            int count = Alt.GetAllPlayers().Count;
            Alt.Log($"[Player Saved Count] {count}");
            foreach (var user in GetAllPlayers())
            {

                if (user.loggedin)
                {
                    var dbPlayer = db.PlayerCharacter.ToList().FirstOrDefault(account => (account.socialclub == user.SocialClubId));
                    if (dbPlayer == null)
                    {
                        Console.WriteLine("Fehler! Diesen Benutzer gibt es nicht! | " + user.SocialClubId);
                        return;
                    }
                    dbPlayer.forumid = user.forumid;
                    dbPlayer.name = user.name;
                    dbPlayer.adminlevel = user.adminlevel;
                    dbPlayer.socialclub = user.socialclub;
                    dbPlayer.money = user.money;
                    dbPlayer.blackmoney = user.blackmoney;
                    dbPlayer.bank = user.bank;
                    dbPlayer.firstname = user.firstname;
                    dbPlayer.lastname = user.lastname;
                    dbPlayer.visum = user.visum;
                    dbPlayer.playhours = user.playhours;
                    dbPlayer.health = user.Health;

                    dbPlayer.armor = user.Armor;
                    dbPlayer.inventory = user.inventory;
                    dbPlayer.gender = user.gender;
                    dbPlayer.bewusstlos = user.bewusstlos;
                    dbPlayer.deathtime = user.deathtime;
                    dbPlayer.hunger = user.hunger;
                    dbPlayer.durst = user.durst;
                    dbPlayer.adresse = user.adresse;
                    dbPlayer.jail = user.jail;
                    dbPlayer.jailtime = user.jailtime;
                    dbPlayer.klogang = user.klogang;
                    dbPlayer.lastlogin = user.lastlogin;
                    dbPlayer.phone = user.phone;
                    dbPlayer.fraktion = user.fraktion;
                    dbPlayer.fraktion_rank = user.fraktion_rank;

                    user.pos_X = user.Position.X;
                    user.pos_Y = user.Position.Y;
                    user.pos_Z = user.Position.Z;

                    dbPlayer.pos_X = user.pos_X;
                    dbPlayer.pos_Y = user.pos_Y;
                    dbPlayer.pos_Z = user.pos_Z;


                    dbPlayer.clothes = user.clothes;
                    dbPlayer.restclothes = user.restclothes;
                    Alt.Log($"[Player Saved] {user.firstname}_{user.lastname}");

                }
            }

            Alt.Log($"[DBSAVED] ALL!");
             db.SaveChangesAsync();
            Console.WriteLine("Stopped");
        }

        public static IEnumerable<DBPlayer> GetAllPlayers()
        {
            return Alt.GetAllPlayers().Cast<DBPlayer>();
        }
    }
}
