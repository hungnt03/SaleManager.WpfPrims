using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaleManager.Wpf.Inflastructor.Models
{
    public class CheckboxModel : BindableBase
    {
        private bool _isChecked;
        private string _label;
        public bool IsChecked
        {
            get { return _isChecked; }
            set{ SetProperty(ref _isChecked, value); }
        }
        public string Label
        {
            get { return _label; }
            set { SetProperty(ref _label, value); }
        }
        public CheckboxModel(string label)
        {
            this.Label = label;
            this.IsChecked = false;
        }
        public CheckboxModel(string label, bool isChecked)
        {
            this.Label = label;
            this.IsChecked = isChecked;
        }
    }
}
