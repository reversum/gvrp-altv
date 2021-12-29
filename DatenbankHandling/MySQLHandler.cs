using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace GVRPALTV.DatenbankHandling
{
    public partial class MySQLHandler : DbContext
    {
        public MySQLHandler()
        {
        }

        public MySQLHandler(DbContextOptions<MySQLHandler> options)
            : base(options)
        {
        }

        public virtual DbSet<PlayerCharacter> PlayerCharacter { get; set; }
        public virtual DbSet<PlayerHouse> PlayerHouse { get; set; }
        public virtual DbSet<Gerichte> Gerichte { get; set; }
       // public virtual DbSet<ItemHandler> ItemHandler { get; set; }
        public virtual DbSet<BlipHandler> BlipHandler { get; set; }
        public virtual DbSet<VehicleHandler> VehicleHandler { get; set; }
        public virtual DbSet<MiniJobDeliveryHandler> MiniJobDeliveryHandler { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseMySql("server=localhost;database=altv;user=root", Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.22-mysql"));
            }
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasCharSet("utf8mb4")
                .UseCollation("utf8mb4_general_ci");

            modelBuilder.Entity<PlayerCharacter>(entity =>
            {
                entity.ToTable("character");

                entity.Property(e => e.id).HasColumnName("id");
                entity.Property(e => e.forumid).HasColumnName("forumid");
                entity.Property(e => e.socialclub).HasColumnName("socialclub").HasMaxLength(255);

                entity.Property(e => e.name).HasColumnName("name").HasMaxLength(255);
                entity.Property(e => e.password).HasColumnName("password").HasMaxLength(64);
                entity.Property(e => e.forumid).HasColumnName("forumid");
                entity.Property(e => e.adminlevel).HasColumnName("adminlevel");
                entity.Property(e => e.hwid).HasColumnName("hwid").HasMaxLength(255);
                entity.Property(e => e.shield).HasColumnName("shield");
                entity.Property(e => e.ban).HasColumnName("ban");
                entity.Property(e => e.bandate).HasColumnName("bandate");
                entity.Property(e => e.bantime).HasColumnName("bantime");
                entity.Property(e => e.firstname).HasColumnName("firstname").HasMaxLength(32);
                entity.Property(e => e.lastname).HasColumnName("lastname").HasMaxLength(32);
                entity.Property(e => e.money).HasColumnName("money").HasMaxLength(255);
                entity.Property(e => e.bank).HasColumnName("bank").HasMaxLength(255);
                entity.Property(e => e.blackmoney).HasColumnName("blackmoney").HasMaxLength(255);
                entity.Property(e => e.visum).HasColumnName("visum");
                entity.Property(e => e.playhours).HasColumnName("playhours");
                entity.Property(e => e.birthday).HasColumnName("birthday");
                entity.Property(e => e.health).HasColumnName("health");
                entity.Property(e => e.armor).HasColumnName("armor");
                entity.Property(e => e.inventory).HasColumnName("inventory").HasMaxLength(255);
                entity.Property(e => e.gender).HasColumnName("gender");
                entity.Property(e => e.character).HasColumnName("character");
                entity.Property(e => e.bewusstlos).HasColumnName("bewusstlos");
                entity.Property(e => e.deathtime).HasColumnName("deathtime");
                entity.Property(e => e.hunger).HasColumnName("hunger");
                entity.Property(e => e.durst).HasColumnName("durst");
                entity.Property(e => e.adresse).HasColumnName("adresse");
                entity.Property(e => e.jail).HasColumnName("jail");
                entity.Property(e => e.jailtime).HasColumnName("jailtime");
                entity.Property(e => e.klogang).HasColumnName("klogang");
                entity.Property(e => e.lastlogin).HasColumnName("lastlogin");
                entity.Property(e => e.phone).HasColumnName("phone");
                entity.Property(e => e.fraktion).HasColumnName("fraktion");
                entity.Property(e => e.fraktion_rank).HasColumnName("fraktion_rank");
                entity.Property(e => e.pos_X).HasColumnName("pos_X");
                entity.Property(e => e.pos_Y).HasColumnName("pos_Y");
                entity.Property(e => e.pos_Z).HasColumnName("pos_Z");


            });
            modelBuilder.Entity<PlayerHouse>(entity =>
            {
                entity.ToTable("houses");

                entity.Property(e => e.id).HasColumnName("id");
                entity.Property(e => e.playerid).HasColumnName("playerid");
                entity.Property(e => e.name).HasColumnName("name").HasMaxLength(255);
                entity.Property(e => e.kueche).HasColumnName("kueche");
                entity.Property(e => e.moebel).HasColumnName("moebel");
                entity.Property(e => e.locked).HasColumnName("locked");
                entity.Property(e => e.mietplaetze).HasColumnName("mietplaetze");
                entity.Property(e => e.preis).HasColumnName("preis");
                entity.Property(e => e.nachricht).HasColumnName("nachricht").HasMaxLength(255);
                entity.Property(e => e.tosell).HasColumnName("tosell");
                entity.Property(e => e.keys).HasColumnName("keys").HasMaxLength(255);
                entity.Property(e => e.kuehlschrank).HasColumnName("kuehlschrank").HasMaxLength(255);
                entity.Property(e => e.kuehlschrankplatz).HasColumnName("kuehlschrankplatz").HasMaxLength(11);
                entity.Property(e => e.schrank).HasColumnName("schrank").HasMaxLength(255);
                entity.Property(e => e.schrankplatz).HasColumnName("schrankplatz").HasMaxLength(11);
                entity.Property(e => e.kleidungen).HasColumnName("kleidungen").HasMaxLength(255);
                entity.Property(e => e.pos_X).HasColumnName("pos_X");
                entity.Property(e => e.pos_Y).HasColumnName("pos_Y");
                entity.Property(e => e.pos_Z).HasColumnName("pos_Z");


            });
            modelBuilder.Entity<Gerichte>(entity =>
            {
                entity.ToTable("food_gerichte");

                entity.Property(e => e.id).HasColumnName("id");
                entity.Property(e => e.name).HasColumnName("name").HasMaxLength(255);
                entity.Property(e => e.zutaten).HasColumnName("zutaten").HasMaxLength(255);
                entity.Property(e => e.zugelassenegerate).HasColumnName("zugelassenegerate").HasMaxLength(255);
                entity.Property(e => e.health).HasColumnName("health");
                entity.Property(e => e.hunger).HasColumnName("hunger");
                entity.Property(e => e.durst).HasColumnName("durst");
                entity.Property(e => e.durchfall).HasColumnName("durchfall");
                entity.Property(e => e.ablaufdatum).HasColumnName("ablaufdatum");
                entity.Property(e => e.klogang).HasColumnName("klogang");



            });
            modelBuilder.Entity<BlipHandler>(entity =>
            {
                entity.ToTable("blip");

                entity.Property(e => e.id).HasColumnName("id");
                entity.Property(e => e.name).HasColumnName("name").HasMaxLength(255);
                entity.Property(e => e.color).HasColumnName("color");
                entity.Property(e => e.scale).HasColumnName("scale");
                entity.Property(e => e.shortrange).HasColumnName("shortrange");
                entity.Property(e => e.sprite).HasColumnName("sprite");
                entity.Property(e => e.pos_x).HasColumnName("pos_x");
                entity.Property(e => e.pos_y).HasColumnName("pos_y");
                entity.Property(e => e.pos_z).HasColumnName("pos_z");



            });
            modelBuilder.Entity<VehicleHandler>(entity =>
            {
                entity.ToTable("vehicles");

                entity.Property(e => e.id).HasColumnName("id");
                entity.Property(e => e.ownerid).HasColumnName("ownerid");
                entity.Property(e => e.name).HasColumnName("name").HasMaxLength(255);
                entity.Property(e => e.plate).HasColumnName("plate").HasMaxLength(255);
                entity.Property(e => e.hash).HasColumnName("hash");
                entity.Property(e => e.price).HasColumnName("price");
                entity.Property(e => e.trunk).HasColumnName("trunk");
                entity.Property(e => e.trunkweight).HasColumnName("trunkweight");
                entity.Property(e => e.MaxFuel).HasColumnName("MaxFuel");
                entity.Property(e => e.pos_X).HasColumnName("pos_X");
                entity.Property(e => e.pos_Y).HasColumnName("pos_Y");
                entity.Property(e => e.pos_Z).HasColumnName("pos_Z");
                entity.Property(e => e.rotation).HasColumnName("rotation");
                entity.Property(e => e.engine).HasColumnName("engine");
                entity.Property(e => e.locked).HasColumnName("locked");
                entity.Property(e => e.health).HasColumnName("health");




            });
            modelBuilder.Entity<MiniJobDeliveryHandler>(entity =>
            {
                entity.ToTable("minijobs_deliverypoints");

                entity.Property(e => e.id).HasColumnName("id");
                entity.Property(e => e.jobname).HasColumnName("jobname").HasMaxLength(255);
                entity.Property(e => e.pos_x).HasColumnName("pos_x");
                entity.Property(e => e.pos_y).HasColumnName("pos_y");
                entity.Property(e => e.pos_z).HasColumnName("pos_z");
    




            });
            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }


}

