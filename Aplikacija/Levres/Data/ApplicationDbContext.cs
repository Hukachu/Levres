using Levres.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Reflection.Emit;

namespace Levres.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Automobil> Automobil { get; set; }
        public DbSet<NoviAutomobil> NoviAutomobil { get; set; }
        public DbSet<PolovniAutomobil> PolovniAutomobil { get; set; }
        public DbSet<Konfiguracija> Konfiguracija { get; set; }
        public DbSet<Narudzba> Narudzba { get; set; }
        public DbSet<Servis> Servis { get; set; }
        public DbSet<Oprema> Oprema { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Automobil>().ToTable("Automobil");
            modelBuilder.Entity<NoviAutomobil>().ToTable("NoviAutomobil");
            modelBuilder.Entity<PolovniAutomobil>().ToTable("PolovniAutomobil");
            modelBuilder.Entity<Konfiguracija>().ToTable("Konfiguracija");
            modelBuilder.Entity<Narudzba>().ToTable("Narudzba");
            modelBuilder.Entity<Servis>().ToTable("Servis");
            modelBuilder.Entity<Oprema>().ToTable("Oprema");
            base.OnModelCreating(modelBuilder);
        }
    }
}
