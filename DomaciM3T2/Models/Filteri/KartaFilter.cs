using System;

namespace DomaciM3T2.Models.Filteri
{
    public class KartaFilter
    {
        public DateTime? DatumOd { get; set; }
        public DateTime? DatumDo { get; set; }
        public double? CenaOd { get; set; }
        public double? CenaDo { get; set; }
    }
}
