using System.Collections.Generic;
using hello.webapi.wpf.Views;

namespace hello.webapi.wpf.Models
{
    public interface IHaveSales
    {
        IList<SaleViewModel> Sales { get; }
    }
}