using System.Collections.Generic;
using System.Linq;
using System;

namespace DomaciM3T2.Models
{
    public class Paginacija<T> : List<T>
    {
        public int IndeksStranice { get; private set; }
        public int UkupnoStranica { get; private set; }

        public Paginacija(List<T> items, int count, int pageIndex, int pageSize)
        {
            IndeksStranice = pageIndex;
            UkupnoStranica = (int)Math.Ceiling(count / (double)pageSize);

            this.AddRange(items);
        }

        public bool HasPreviousPage()
        {
            return (IndeksStranice > 0);
        }

        public bool HasNextPage()
        {
            return (IndeksStranice < UkupnoStranica - 1);
        }

        public static Paginacija<T> Create(IQueryable<T> source, int pageIndex, int pageSize)
        {
            var count = source.Count();
            var items = source.Skip(pageIndex * pageSize).Take(pageSize).ToList();
            return new Paginacija<T>(items, count, pageIndex, pageSize);
        }
    }
}
