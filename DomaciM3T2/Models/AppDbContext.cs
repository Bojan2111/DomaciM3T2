using Microsoft.EntityFrameworkCore;
using System;

namespace DomaciM3T2.Models
{
    public class AppDbContext : DbContext
    {
        public DbSet<Festival> Festivals { get; set; }
        public DbSet<Karta> Kartas { get; set; }
        public DbSet<TipKarte> TipKartes { get; set; }
        
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Festival>().HasData(
                new Festival() { Id = 1, Naziv = "Svetsko prvenstvo u nadmetanju guskova", Mesto = "Mokrin", DatumOdrzavanja = new DateTime(2023, 2, 26), Ocena = 4, MaksimumPosetilaca = 6000 },
                new Festival() { Id = 2, Naziv = "Nisville", Mesto = "Nis", DatumOdrzavanja = new DateTime(2023, 8, 4), Ocena = 5, MaksimumPosetilaca = 150000 },
                new Festival() { Id = 3, Naziv = "Beer Fest", Mesto = "Beograd", DatumOdrzavanja = new DateTime(2023, 8, 18), Ocena = 4, MaksimumPosetilaca = 100000 }
            );

            modelBuilder.Entity<TipKarte>().HasData(
                new TipKarte() { Id = 1, Naziv = "Redovna karta" },
                new TipKarte() { Id = 2, Naziv = "VIP karta" }
            );

            modelBuilder.Entity<Karta>().HasData(
                new Karta() { Id = 1, Cena = 1000, DatumKupovine = new DateTime(2023, 2, 23), Kupac = "Pera Peric", FestivalId = 1, TipKarteId = 1, Preuzeta = true },
                new Karta() { Id = 2, Cena = 2000, DatumKupovine = new DateTime(2023, 2, 21), Kupac = "Djoka Djokic", FestivalId = 1, TipKarteId = 2, Preuzeta = false },
                new Karta() { Id = 3, Cena = 1800, DatumKupovine = new DateTime(2023, 2, 20), Kupac = "Marko Markovic", FestivalId = 2, TipKarteId = 1, Preuzeta = false },
                new Karta() { Id = 4, Cena = 2600, DatumKupovine = new DateTime(2023, 3, 13), Kupac = "Lara Laric", FestivalId = 2, TipKarteId = 2, Preuzeta = true },
                new Karta() { Id = 5, Cena = 1560, DatumKupovine = new DateTime(2023, 4, 28), Kupac = "Mika Mikic", FestivalId = 3, TipKarteId = 1, Preuzeta = true },
                new Karta() { Id = 6, Cena = 2820, DatumKupovine = new DateTime(2023, 1, 25), Kupac = "Ana Ancic", FestivalId = 3, TipKarteId = 2, Preuzeta = false }
                );

            base.OnModelCreating(modelBuilder);
        }
    }
}
