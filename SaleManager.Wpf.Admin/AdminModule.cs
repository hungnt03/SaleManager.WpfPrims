﻿using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;
using SaleManager.Wpf.Admin.Views;
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

            regionManager.RegisterViewWithRegion("ContentRegion", typeof(AccountCreateView));

            regionManager.RegisterViewWithRegion("ContentMenuRegion", typeof(CategoryListView));
            regionManager.RequestNavigate("ContentMenuRegion", nameof(CategoryListView));
            //regionManager.RegisterViewWithRegion("ContentMenuRegion", typeof(CategoryView));
            //regionManager.RequestNavigate("ContentMenuRegion", nameof(CategoryView));
            regionManager.RequestNavigate("ContentMenuRegion", nameof(AccountListView));
            //regionManager.RequestNavigate("ContentMenuRegion", nameof(AccountView));
            

            //regionManager.RegisterViewWithRegion("ContentMenuRegion", typeof(CustomerView));
            regionManager.RegisterViewWithRegion("ContentMenuRegion", typeof(ConfirmDialogView));
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<CategoryListView>();
            containerRegistry.RegisterForNavigation<CategoryView>();
            containerRegistry.RegisterForNavigation<AccountListView>();
            containerRegistry.RegisterForNavigation<AccountView>();

            containerRegistry.RegisterForNavigation<AccountCreateView>();
        }
    }
}
