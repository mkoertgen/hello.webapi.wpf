using System.Collections.Generic;

namespace hello.webapi.wpf
{
    public interface IHaveSales
    {
        IList<SaleViewModel> Sales { get; }
    }
}