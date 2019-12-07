using SaleManager.Wpf.Inflastructor;
using System.ComponentModel.DataAnnotations;

namespace SaleManager.Wpf.Admin.Models
{
    public class CustomerModel : AddBindableBase
    {
        private int _id;
        private string _name;
        private string _address;
        private string _contact;

        public int Id
        {
            get { return _id; }
            set { SetProperty(ref _id, value); }
        }
        [StringLength(50, ErrorMessage = "Vui lòng nhập dưới {1} ký tự.")]
        [Required(ErrorMessage = "Trường không được để trống")]
        [Display(Name = "Tên khách hàng")]
        public string Name
        {
            get { return _name; }
            set
            {
                SetProperty(ref _name, value);
                this.ValidateProperty(value);
            }
        }

        [StringLength(50, ErrorMessage = "Vui lòng nhập dưới {1} ký tự.")]
        [Display(Name = "Địa chỉ")]
        public string Address
        {
            get { return _address; }
            set
            {
                SetProperty(ref _address, value);
                this.ValidateProperty(value);
            }
        }

        [StringLength(50, ErrorMessage = "Vui lòng nhập dưới {1} ký tự.")]
        [Display(Name = "Số Điện thoại")]
        public string Contact
        {
            get { return _contact; }
            set
            {
                SetProperty(ref _contact, value);
                this.ValidateProperty(value);
            }
        }
    }
}
