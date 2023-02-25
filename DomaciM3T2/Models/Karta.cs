using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace DomaciM3T2.Models
{
    public class Karta
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Cena karte mora biti uneta")]
        [Range(100, 10000, ErrorMessage = "Cena karte mora biti u rasponu od 100 do 10 000 RSD")]
        public double Cena { get; set; }
        [DisplayName("Datum Kupovine")]
        public DateTime DatumKupovine { get; set; }
        [Required(ErrorMessage = "Ime i prezime kupca mora biti uneto")]
        public string Kupac { get; set; }
        public bool Preuzeta { get; set; }
        public int TipKarteId { get; set; }
        [DisplayName("Tip karte")]
        public TipKarte TipKarte { get; set; }
        public int FestivalId { get; set; }
        public Festival Festival { get; set; }
    }
}
