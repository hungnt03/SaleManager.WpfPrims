using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaleManager.Wpf.ViewModels.Menu
{
    public class MenuDetailViewModel : BindableBase
    {
        public string Title { get; }
        public string IconName { get; }
        public string ScreenName { get; }
        public MenuDetailViewModel(string screenName, string title, string iconName)
        {
            Title = title;
            IconName = iconName;
            ScreenName = screenName;
        }
    }
}
