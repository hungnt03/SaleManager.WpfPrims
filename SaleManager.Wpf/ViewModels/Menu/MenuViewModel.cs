using Caliburn.Micro;
using MahApps.Metro.Controls;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using SaleManager.Wpf.Admin.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace SaleManager.Wpf.ViewModels.Menu
{
    public class MenuViewModel : BindableBase
    {
        public BindableCollection<MenuDetailViewModel> Menus { get; }
        private MenuDetailViewModel _selectedView;
        private readonly IRegionManager _regionManager;
        public MenuDetailViewModel SelectedView
        {
            get { return _selectedView; }
            set 
            { 
                SetProperty(ref _selectedView, value);
                _regionManager.RequestNavigate("ContentMenuRegion", value.ScreenName);
            }
        }
        public MenuViewModel(IRegionManager regionManager)
        {
            _regionManager = regionManager;

            Menus = new BindableCollection<MenuDetailViewModel>()
            {
                new MenuDetailViewModel("CategoryListView", "Category", "BrandingWatermark"),
                new MenuDetailViewModel("CustomerView", "Customer", "HumanMaleFemale")
            };
        }

    }
}
