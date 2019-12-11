using SaleManager.Wpf.Inflastructor;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace SaleManager.Wpf.Admin.Models
{
    public class ProductModel : AddBindableBase
    {
        private string _barcode;
        private string _name;
        private Decimal _price;
        private DateTime _expirationDate;
        private int _categoryId;
        private int _supplierId;
        private BitmapImage _imageBit;
        private string _img;
        private int _quantity;

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
            }
        }
        public BitmapImage ImageBit
        {
            get { return _imageBit; }
            set
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
        [RegularExpression("([0-9]+)",ErrorMessage = "Có ký tự không hợp lệ")]
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
