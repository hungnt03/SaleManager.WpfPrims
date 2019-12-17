using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SaleManager.Wpf.Inflastructor.Helpers
{
    public static class NumberHelpers
    {
        public static bool IsNumber(object param)
        {
            Regex regex = new Regex(@"^\d$");
            if (regex.IsMatch(param.ToString()))
                return true;
            return false;
        }
        
    }
}
