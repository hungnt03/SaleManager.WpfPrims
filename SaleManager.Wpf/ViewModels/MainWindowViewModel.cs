using Prism.Ioc;
using Prism.Mvvm;
using Prism.Regions;
using SaleManager.Wpf.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaleManager.Wpf.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        private string _title = "Prism Unity Application";
        IContainerExtension _container;
        IRegionManager _regionManager;
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        public MainWindowViewModel(IContainerExtension container, IRegionManager regionManager)
        {
            _container = container;
            _regionManager = regionManager;
            _regionManager.RegisterViewWithRegion("ContentRegion", typeof(LoginView));
        }
    }
}
