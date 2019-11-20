﻿using SaleManager.Wpf.Inflastructor;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaleManager.Wpf.Admin.Models
{
    public class CustomerModel : AddBindableBase
    {
        private int _id;
        private string _name;
        private string _description;

        public int Id
        {
            get { return _id; }
            set { SetProperty(ref _id, value); }
        }
        [StringLength(50, ErrorMessage = "Vui lòng nhập dưới {1} ký tự.")]
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
    }
}
