using System.Collections.Generic;
using System.Linq;

namespace hello.webapi.wpf
{
    static class SaleExtensions
    {
        public static IEnumerable<Sale> ToModel(this IEnumerable<SaleViewModel> sales)
        {
            return sales.Select(ToModel);
        }

        public static Sale ToModel(this SaleViewModel model)
        {
            return new Sale {Id = model.Id, Buyer = model.Buyer};
        }

        public static SaleViewModel ToViewModel(this Sale model)
        {
            return new SaleViewModel { Id = model.Id, Buyer = model.Buyer };
        }

        public static void FromModel(this SaleViewModel viewModel, Sale model)
        {
            viewModel.Id = model.Id;
            viewModel.Buyer = model.Buyer;
        }
    }
}