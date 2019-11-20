using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaleManager.Wpf.Models
{
    public class UserModel
    {
        public string Firstname { set; get; }
        public string Lastname { set; get; }
        public string Id { set; get; }

        public string GetName()
        {
            return Firstname + " " + Lastname;
        }
    }
}
