using System;

namespace DomaciM3T2.Models.Filteri
{
    public class FestivalFilter
    {
        public string Naziv { get; set; }
        public string Mesto { get; set; }
        public DateTime? DatumOd { get; set; }
        public DateTime? DatumDo { get; set; }
        public int? Ocena { get; set; }
    }
}
