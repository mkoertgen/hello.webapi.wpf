using Caliburn.Micro;
using hello.webapi.wpf.Models;

namespace hello.webapi.wpf.Views
{
    public class SaleViewModel : PropertyChangedBase, ISale
    {
        private string _buyer;
        private int _id;

        public int Id
        {
            get => _id;
            set
            {
                if (value == _id) return;
                _id = value;
                NotifyOfPropertyChange();
            }
        }

        public string Buyer
        {
            get => _buyer;
            set
            {
                if (value == _buyer) return;
                _buyer = value;
                NotifyOfPropertyChange();
            }
        }
    }
}