namespace hello.webapi.wpf
{
    public interface ISalesViewModel : IHaveSales
    {
        SaleViewModel SelectedSale { get; set; }
    }
}