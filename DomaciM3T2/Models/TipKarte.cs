using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DomaciM3T2.Models
{
    public class TipKarte
    {
        public int Id { get; set; }
        [Required]
        [StringLength(100)]
        public string Naziv { get; set; }
        public ICollection<Karta> Kartas { get; set; }
    }
}
