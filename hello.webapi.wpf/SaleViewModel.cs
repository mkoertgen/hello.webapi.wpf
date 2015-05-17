using Caliburn.Micro;

namespace hello.webapi.wpf
{
    public class SaleViewModel : PropertyChangedBase, ISale
    {
        private int _id;
        private string _buyer;

        public int Id
        {
            get { return _id; }
            set
            {
                if (value == _id) return;
                _id = value;
                NotifyOfPropertyChange();
            }
        }

        public string Buyer
        {
            get { return _buyer; }
            set
            {
                if (value == _buyer) return;
                _buyer = value;
                NotifyOfPropertyChange();
            }
        }
    }
}