using Prism.Commands;
using Prism.Regions;
using Prism.Services.Dialogs;
using SaleManager.Wpf.Admin.Models;
using SaleManager.Wpf.Admin.Views;
using SaleManager.Wpf.Inflastructor;
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
        private ObservableCollection<CustomerModel> _customers;
        private CustomerModel _selectedItem;
        private readonly IRegionManager _regionManager;
        public DelegateCommand OnCreate { get; private set; }
        public ObservableCollection<CustomerModel> Customers
        {
            get { return _customers; }
            set
            {
                SetProperty(ref _customers, value);
            }
        }
        public CustomerModel SelectedItem
        {
            get { return _selectedItem; }
            set
            {
                SetProperty(ref _selectedItem, value);
                var parameters = new NavigationParameters();
                parameters.Add("category", value);
                _regionManager.RequestNavigate("ContentMenuRegion", "CustomerView", parameters);
            }
        }

        public CustomerListViewModel(IRegionManager regionManager, IDialogService dialogService) : base(dialogService)
        {
            _regionManager = regionManager;
            OnCreate = new DelegateCommand(Create);
            InitList();
        }
        private void Create()
        {
            Action a = () =>
            {
                _regionManager.RequestNavigate("ContentMenuRegion", nameof(CustomerView));
            };
            ExecuteAction(a);
        }
        private async void InitList()
        {
            Customers = new ObservableCollection<CustomerModel>();
            var datas = await RestApiUtils.Instance.Get<List<CustomerModel>>("api/customer/getall");
            //var datas = Newtonsoft.Json.JsonConvert.DeserializeObject<List<CustomerModel>>(json.Data);
            foreach (var elm in datas)
            {
                //Customers.Add(new CustomerModel(elm.Id, elm.Name, elm.Description));
            }
        }

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            InitList();
        }

        public bool IsNavigationTarget(NavigationContext navigationContext) => true;

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {

        }
    }
}
