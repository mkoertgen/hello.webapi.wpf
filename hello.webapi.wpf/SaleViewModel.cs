using Caliburn.Micro;

namespace hello.webapi.wpf
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