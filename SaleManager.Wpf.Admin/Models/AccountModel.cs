using SaleManager.Wpf.Inflastructor;
using SaleManager.Wpf.Inflastructor.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaleManager.Wpf.Admin.Models
{
    public class AccountModel : AddBindableBase
    {
        private string _id;
        private string _username;
        private string _email;
        private string _firstName;
        private string _lastName;
        private byte _level;
        private DateTime _joinDate;
        private bool _isEnable;
        private ObservableCollection<CheckboxModel> _roles;

        public AccountModel()
        {
            Roles = new ObservableCollection<CheckboxModel>();
            
        }
        public ObservableCollection<CheckboxModel> Roles
        {
            get { return _roles; }
            set
            {
                SetProperty(ref _roles, value);
                this.ValidateProperty(value);
            }
        }
        public bool IsEnable
        {
            get { return _isEnable; }
            set
            {
                SetProperty(ref _isEnable, value);
                this.ValidateProperty(value);
            }
        }
        public DateTime JoinDate
        {
            get { return _joinDate; }
            set
            {
                SetProperty(ref _joinDate, value);
                this.ValidateProperty(value);
            }
        }

        public byte Level
        {
            get { return _level; }
            set
            {
                SetProperty(ref _level, value);
                this.ValidateProperty(value);
            }
        }

        public string LastName
        {
            get { return _lastName; }
            set
            {
                SetProperty(ref _lastName, value);
                this.ValidateProperty(value);
            }
        }

        public string FirstName
        {
            get { return _firstName; }
            set
            {
                SetProperty(ref _firstName, value);
                this.ValidateProperty(value);
            }
        }

        public string Email
        {
            get { return _email; }
            set
            {
                SetProperty(ref _email, value);
                this.ValidateProperty(value);
            }
        }

        public string Username
        {
            get { return _username; }
            set
            {
                SetProperty(ref _username, value);
                this.ValidateProperty(value);
            }
        }
        public string Id
        {
            get { return _id; }
            set
            {
                SetProperty(ref _id, value);
                this.ValidateProperty(value);
            }
        }
    }
}
