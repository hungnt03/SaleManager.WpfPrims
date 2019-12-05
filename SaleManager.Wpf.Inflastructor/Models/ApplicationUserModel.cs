using System.Collections.Generic;

namespace SaleManager.Wpf.Inflastructor.Models
{
    public class ApplicationUserModel
    {
        public UserModel User { set; get; }
        public List<string> Roles { set; get; }
    }
}
