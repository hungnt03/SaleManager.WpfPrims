using Prism.Commands;
using Prism.Ioc;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaleManager.Wpf.ViewModels
{
    public class HeaderViewModel : BindableBase
    {
        IContainerExtension _container;
        IRegionManager _regionManager;
        public string Username;
        public DelegateCommand OnAccountSetting { get; private set; }
        public HeaderViewModel(IContainerExtension container, IRegionManager regionManager)
        {
            _container = container;
            _regionManager = regionManager;
            Username =  MainWindowViewModel.CurrentUser.User.GetName();
        }
    }
}
