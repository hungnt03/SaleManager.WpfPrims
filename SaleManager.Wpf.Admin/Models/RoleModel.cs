using SaleManager.Wpf.Inflastructor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaleManager.Wpf.Admin.Models
{
    public class RoleModel : AddBindableBase
    {
        private string _id;
        private string _role;
        public string Role
        {
            get { return _role; }
            set
            {
                SetProperty(ref _role, value);
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
