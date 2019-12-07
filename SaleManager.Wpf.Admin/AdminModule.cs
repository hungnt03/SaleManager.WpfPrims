using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;
using SaleManager.Wpf.Admin.Views;
using SaleManager.Wpf.Admin.Views.Menu;
using SaleManager.Wpf.Inflastructor.ViewModels;
using SaleManager.Wpf.Inflastructor.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaleManager.Wpf.Admin
{
    public class AdminModule : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider)
        {
            var regionManager = containerProvider.Resolve<IRegionManager>();
            regionManager.RequestNavigate("ContentRegion", nameof(LoginView));

            //regionManager.RegisterViewWithRegion("ContentMenuRegion", typeof(CategoryListView));
            //regionManager.RequestNavigate("ContentMenuRegion", nameof(CategoryListView));
            //regionManager.RequestNavigate("ContentMenuRegion", nameof(CategoryView));
            //regionManager.RequestNavigate("ContentMenuRegion", nameof(AccountListView));
            //regionManager.RequestNavigate("ContentMenuRegion", nameof(AccountView));


            //regionManager.RegisterViewWithRegion("ContentMenuRegion", typeof(CustomerView));
            //regionManager.RegisterViewWithRegion("ContentMenuRegion", typeof(ConfirmDialogView));
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<HeaderView>();
            containerRegistry.RegisterForNavigation<LoginView>();
            containerRegistry.RegisterForNavigation<MenuView>();
            containerRegistry.RegisterForNavigation<EmptyView>();
            containerRegistry.RegisterForNavigation<CategoryListView>();
            containerRegistry.RegisterForNavigation<CategoryView>();
            containerRegistry.RegisterForNavigation<AccountListView>();
            containerRegistry.RegisterForNavigation<AccountView>();
            containerRegistry.RegisterForNavigation<AccountCreateView>();
            containerRegistry.RegisterForNavigation<CustomerListView>();
            containerRegistry.RegisterForNavigation<CustomerView>();

            containerRegistry.RegisterDialog<ConfirmDialogView, ConfirmDialogViewModel>();
        }
    }
}
