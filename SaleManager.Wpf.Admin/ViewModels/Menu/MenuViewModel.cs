using Caliburn.Micro;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using SaleManager.Wpf.Admin.ViewModels;
using SaleManager.Wpf.Admin.Views;
using SaleManager.Wpf.Inflastructor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace SaleManager.Wpf.Admin.ViewModels.Menu
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
                _regionManager.RequestNavigate("ContentRegion", value.ScreenName);
            }
        }
        public MenuViewModel(IRegionManager regionManager)
        {
            _regionManager = regionManager;

            Menus = new BindableCollection<MenuDetailViewModel>()
            {
                new MenuDetailViewModel(nameof(CategoryListView), "Category", "BrandingWatermark"),
                new MenuDetailViewModel(nameof(CustomerListView), "Customer", "ContactPhoneOutline"),
                new MenuDetailViewModel(nameof(ProductListView), "Product", "Cocktail"),
            };
            if (RestApiUtils.CurrentUser.Roles.Contains("admin"))
                Menus.Add(new MenuDetailViewModel("AccountListView", "Account", "AccountTie"));
        }

    }
}
