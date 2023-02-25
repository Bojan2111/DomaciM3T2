using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace DomaciM3T2.Models
{
    public class Festival
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Naziv festivala je obavezno polje")]
        [StringLength(256, MinimumLength = 3, ErrorMessage = "Naziv mora biti duzine izmedju 3 i 256 karaktera")]
        public string Naziv { get; set; }
        [Required(ErrorMessage = "Mesto odrzavanja festivala je obavezno polje")]
        public string Mesto { get; set; }
        [DisplayName("Datum odrzavanja")]
        public DateTime DatumOdrzavanja { get; set; }
        public int Ocena { get; set; }
        [Range(0, 2000000, ErrorMessage = "Broj posetilaca ne moze biti veci od 2 000 000")]
        [DisplayName("Maksimum posetilaca")]
        public int MaksimumPosetilaca { get; set; }
        public ICollection<Karta> Kartas { get; set; }
    }
}
