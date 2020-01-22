using System.Collections.Generic;
using System.Linq;
using Caliburn.Micro;

namespace hello.webapi.wpf
{
    public class SalesViewModel : Screen, ISalesViewModel
    {
        private readonly BindableCollection<SaleViewModel> _sales = new BindableCollection<SaleViewModel>
        {
            new SaleViewModel {Id = 4645, Buyer = "John Smith"}, new SaleViewModel {Id = 23455, Buyer = "Mark Johnson"}
        };

        private SaleViewModel _selectedSale;


        public IList<SaleViewModel> Sales
        {
            get => _sales;
            set
            {
                _sales.Clear();
                if (value != null) _sales.AddRange(value);
                SelectedSale = _sales.FirstOrDefault();
            }
        }

        public SaleViewModel SelectedSale
        {
            get => _selectedSale;
            set
            {
                if (Equals(value, _selectedSale)) return;
                _selectedSale = value;
                NotifyOfPropertyChange();
            }
        }
    }
}