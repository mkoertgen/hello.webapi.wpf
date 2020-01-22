using hello.webapi.wpf.Models;

namespace hello.webapi.wpf.Views
{
    public interface ISalesViewModel : IHaveSales
    {
        SaleViewModel SelectedSale { get; set; }
    }
}