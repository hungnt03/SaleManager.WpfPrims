using SaleManager.Wpf.Inflastructor;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaleManager.Wpf.Admin.Models
{
    class AccountCreateModel : AddBindableBase
    {
        private string _username;
        private string _password;
        private string _confirmPassword;
        private string _email;
        private string _firstName;
        private string _lastName;
        private byte _level;
        private DateTime _joinDate;

        public DateTime JoinDate
        {
            get { return _joinDate; }
            set
            {
                SetProperty(ref _joinDate, value);
                this.ValidateProperty(value);
            }
        }

        [Required(ErrorMessage = "Trường không được để trống")]
        [MaxLength(1)]
        [RegularExpression("[^0-9]", ErrorMessage = "Vui lòng chỉ nhập ký tự số")]
        public byte Level
        {
            get { return _level; }
            set
            {
                SetProperty(ref _level, value);
                this.ValidateProperty(value);
            }
        }

        [Required(ErrorMessage = "Trường không được để trống")]
        [StringLength(100, ErrorMessage = "Vui lòng nhập dưới {1} ký tự.")]
        public string LastName
        {
            get { return _lastName; }
            set
            {
                SetProperty(ref _lastName, value);
                this.ValidateProperty(value);
            }
        }

        [Required(ErrorMessage = "Trường không được để trống")]
        [StringLength(100, ErrorMessage = "Vui lòng nhập dưới {1} ký tự.")]
        public string FirstName
        {
            get { return _firstName; }
            set
            {
                SetProperty(ref _firstName, value);
                this.ValidateProperty(value);
            }
        }

        [Required(ErrorMessage = "Trường không được để trống")]
        [EmailAddress]
        public string Email
        {
            get { return _email; }
            set
            {
                SetProperty(ref _email, value);
                this.ValidateProperty(value);
            }
        }

        [Compare("Password", ErrorMessage = "Xác nhận mật khẩu và mật khẩu phải trùng nhau")]
        [DataType(DataType.Password)]
        public string ConfirmPassword
        {
            get { return _confirmPassword; }
            set
            {
                SetProperty(ref _confirmPassword, value);
                this.ValidateProperty(value);
            }
        }

        [StringLength(100, ErrorMessage = "Mật khẩu phải trên {0} ký tự và dưới {1} ký tự", MinimumLength = 6)]
        [Required(ErrorMessage = "Trường không được để trống")]
        [DataType(DataType.Password)]
        public string Password
        {
            get { return _password; }
            set
            {
                SetProperty(ref _password, value);
                this.ValidateProperty(value);
            }
        }

        [RegularExpression("[0-9a-z]+", ErrorMessage = "Vui lòng nhập số và chữ thường.")]
        [Required(ErrorMessage = "Trường không được để trống")]
        public string Username
        {
            get { return _username; }
            set
            {
                SetProperty(ref _username, value);
                this.ValidateProperty(value);
            }
        }
    }
}
