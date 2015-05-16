using System.Collections.Generic;
using System.Linq;

namespace hello.webapi.wpf
{
    static class SaleExtensions
    {
        public static IEnumerable<Sale> ToModel(this IEnumerable<ISale> sales)
        {
            return sales.Select(s => new Sale {Id = s.Id, Buyer = s.Buyer});
        }
    }
}