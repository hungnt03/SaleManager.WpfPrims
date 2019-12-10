using SaleManager.Wpf.Inflastructor;

namespace SaleManager.Wpf.Admin.Models
{
    public class SupplierModel : AddBindableBase
    {
        private int _id;
        private string _name;
        private string _address;
        private string _contact1;
        private string _contact2;

        public int Id
        {
            get { return _id; }
            set { SetProperty(ref _id, value); }
        }
        public string Name
        {
            get { return _name; }
            set { SetProperty(ref _name, value); }
        }
        public string Address
        {
            get { return _address; }
            set { SetProperty(ref _address, value); }
        }
        public string Contact1
        {
            get { return _contact1; }
            set { SetProperty(ref _contact1, value); }
        }
        public string Contact2
        {
            get { return _contact2; }
            set { SetProperty(ref _contact2, value); }
        }
    }
}
