using MaterialDesignThemes.Wpf;
using Prism.Events;
using Prism.Ioc;
using Prism.Mvvm;
using Prism.Regions;
using SaleManager.Wpf.Admin.Views;
using SaleManager.Wpf.Inflastructor.Models;
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
        IEventAggregator _ea;
        
        private static SnackbarMessageQueue _snackbar;
        public SnackbarMessageQueue Snackbar
        {
            get { return _snackbar; }
            set { SetProperty(ref _snackbar, value); }
        }
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        public MainWindowViewModel(IContainerExtension container, 
            IRegionManager regionManager, IEventAggregator ea)
        {
            _container = container;
            _regionManager = regionManager;
            _ea = ea;

            //_regionManager.RegisterViewWithRegion("ContentRegion", typeof(LoginView));
            _ea.GetEvent<NotifSentEvent>().Subscribe(ShowNotification);
            Snackbar = new SnackbarMessageQueue();
            Snackbar.Enqueue("Version 1.0");
        }

        public void ShowNotification(string notif)
        {
            Snackbar.Enqueue(notif);
        }
    }
}
