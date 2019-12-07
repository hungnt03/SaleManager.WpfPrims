using Prism.Commands;
using Prism.Regions;
using Prism.Services.Dialogs;
using SaleManager.Wpf.Admin.Models;
using SaleManager.Wpf.Admin.Views;
using SaleManager.Wpf.Inflastructor;
using SaleManager.Wpf.Inflastructor.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaleManager.Wpf.Admin.ViewModels
{
    public class CustomerListViewModel : ViewModelBase, INavigationAware
    {
        public ObservableCollection<CustomerModel> _customers;
        private readonly IRegionManager _regionManager;
        IRegionNavigationJournal _journal;
        public DelegateCommand OnCreate { get; private set; }
        public DelegateCommand<CustomerModel> SelectedCommand { get; private set; }
        public ObservableCollection<CustomerModel> Customers
        {
            get { return _customers; }
            set
            {
                SetProperty(ref _customers, value);
            }
        }
        public CustomerListViewModel(IRegionManager regionManager, IDialogService dialogService) : base(dialogService)
        {
            _regionManager = regionManager;
            OnCreate = new DelegateCommand(Create);
            SelectedCommand = new DelegateCommand<CustomerModel>(CustomerSelected);
        }
        private void CustomerSelected(CustomerModel customer)
        {
            var parameters = new NavigationParameters();
            parameters.Add("customer", customer);
            if (customer != null)
                _regionManager.RequestNavigate(RegionNames.ContentRegion, nameof(CustomerView), parameters);
        }
        private void Create()
        {
            Action a = () =>
            {
                _regionManager.RequestNavigate(RegionNames.ContentRegion, nameof(CustomerView));
            };
            ExecuteAction(a);
        }
        private async void InitList()
        {
            var datas = await RestApiUtils.Instance.Get<List<CustomerModel>>("api/customer/getall");
            Customers = new ObservableCollection<CustomerModel>(datas);
        }

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            _journal = navigationContext.NavigationService.Journal;
            InitList();
        }

        public bool IsNavigationTarget(NavigationContext navigationContext) => true;

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
        }
    }
}
