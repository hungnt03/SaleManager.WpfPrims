﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaleManager.Wpf.Models
{
    public class ApplicationUserModel
    {
        public UserModel User { set; get; }
        public List<string> Roles { set; get; }
    }
}
