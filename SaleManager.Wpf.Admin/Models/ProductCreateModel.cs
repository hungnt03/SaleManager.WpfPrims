using SaleManager.Wpf.Inflastructor;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace SaleManager.Wpf.Admin.Models
{
    public class ProductCreateModel : AddBindableBase
    {
        private string _barcode;
        private string _name;
        private int _quantity;
        private Decimal _price;
        private int _categoryId;
        private int _supplierId;
        private bool _pin;
        private bool _enable;
        private DateTime _expirationDate;
        private string _unit;
        private BitmapImage _imageBit;
        private string _img;
        private ObservableCollection<CategoryModel> _categories;
        private CategoryModel _categorySelected;
        private ObservableCollection<SupplierModel> _suppliers;
        private SupplierModel _supplierSelected;
        public ProductCreateModel()
        {
            ExpirationDate = DateTime.Now;
        }
        public SupplierModel SupplierSelected
        {
            get { return _supplierSelected; }
            set
            {
                SetProperty(ref _supplierSelected, value);
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

        public string Unit
        {
            get { return _unit; }
            set
            {
                SetProperty(ref _unit, value);
                this.ValidateProperty(value);
            }
        }
        public bool Enable
        {
            get { return _enable; }
            set
            {
                SetProperty(ref _enable, value);
                this.ValidateProperty(value);
            }
        }
        public bool Pin
        {
            get { return _pin; }
            set
            {
                SetProperty(ref _pin, value);
                this.ValidateProperty(value);
            }
        }

        [Required(ErrorMessage = "Trường không được để trống")]
        [RegularExpression("([0-9]+)", ErrorMessage = "Có ký tự không hợp lệ")]
        public int Quantity
        {
            get { return _quantity; }
            set
            {
                SetProperty(ref _quantity, value);
                this.ValidateProperty(value);
            }
        }
        public string Img
        {
            get { return _img; }
            set
            {
                SetProperty(ref _img, value);
                this.ValidateProperty(value);
                if (!string.IsNullOrEmpty(value))
                    ImageBit = new BitmapImage(new Uri(value));
            }
        }
        public BitmapImage ImageBit
        {
            get { return _imageBit; }
            private set
            {
                SetProperty(ref _imageBit, value);
                this.ValidateProperty(value);
            }
        }
        [Required(ErrorMessage = "Trường không được để trống")]
        [RegularExpression("([0-9]+)", ErrorMessage = "Có ký tự không hợp lệ")]
        public int SupplierId
        {
            get { return _supplierId; }
            set
            {
                SetProperty(ref _supplierId, value);
                this.ValidateProperty(value);
            }
        }
        [Required(ErrorMessage = "Trường không được để trống")]
        [RegularExpression("([0-9]+)", ErrorMessage = "Có ký tự không hợp lệ")]
        public int CategoryId
        {
            get { return _categoryId; }
            set
            {
                SetProperty(ref _categoryId, value);
                this.ValidateProperty(value);
            }
        }
        public DateTime ExpirationDate
        {
            get { return _expirationDate; }
            set
            {
                SetProperty(ref _expirationDate, value);
                this.ValidateProperty(value);
            }
        }
        [Required(ErrorMessage = "Trường không được để trống")]
        public Decimal Price
        {
            get { return _price; }
            set
            {
                SetProperty(ref _price, value);
                this.ValidateProperty(value);
            }
        }
        [Required(ErrorMessage = "Trường không được để trống")]
        public string Name
        {
            get { return _name; }
            set
            {
                SetProperty(ref _name, value);
                this.ValidateProperty(value);
            }
        }

        [StringLength(13, ErrorMessage = "Vui lòng nhập {1} ký tự số.")]
        [RegularExpression("([0-9]+)", ErrorMessage = "Có ký tự không hợp lệ")]
        public string Barcode
        {
            get { return _barcode; }
            set
            {
                SetProperty(ref _barcode, value);
                this.ValidateProperty(value);
            }
        }
    }
}
