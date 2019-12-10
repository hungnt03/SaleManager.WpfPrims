using SaleManager.Wpf.Inflastructor;
using System.Collections.ObjectModel;

namespace SaleManager.Wpf.Admin.Models
{
    public class ProductSearchModel : AddBindableBase
    {
        private string _barcodeOrName;
        private ObservableCollection<CategoryModel> _categories;
        private CategoryModel _categorySelected;
        private ObservableCollection<SupplierModel> _suppliers;
        private SupplierModel _supplierSelected;
        public SupplierModel SupplierSelected
        {
            get { return _supplierSelected; }
            set
            {
                SetProperty(ref _supplierSelected, value);
                this.ValidateProperty(value);
            }
        }
        public ObservableCollection<SupplierModel> Suppliers
        {
            get { return _suppliers; }
            set
            {
                SetProperty(ref _suppliers, value);
                this.ValidateProperty(value);
            }
        }
        public CategoryModel CategorySelected
        {
            get { return _categorySelected; }
            set
            {
                SetProperty(ref _categorySelected, value);
                this.ValidateProperty(value);
            }
        }
        public ObservableCollection<CategoryModel> Categories
        {
            get { return _categories; }
            set
            {
                SetProperty(ref _categories, value);
                this.ValidateProperty(value);
            }
        }
        public string BarcodeOrName
        {
            get { return _barcodeOrName; }
            set
            {
                SetProperty(ref _barcodeOrName, value);
                this.ValidateProperty(value);
            }
        }
    }
}
