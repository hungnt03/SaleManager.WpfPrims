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
    public class AccountListViewModel : ViewModelBase, INavigationAware
    {
        private ObservableCollection<AccountModel> _accounts;
        private AccountModel _selectedItem;
        private readonly IRegionManager _regionManager;
        public ObservableCollection<AccountModel> Categories
        {
            get { return _accounts; }
            set
            {
                SetProperty(ref _accounts, value);
            }
        }
        public AccountModel SelectedItem
        {
            get { return _selectedItem; }
            set
            {
                SetProperty(ref _selectedItem, value);
                var parameters = new NavigationParameters();
                parameters.Add("account", value);
                _regionManager.RequestNavigate("ContentMenuRegion", nameof(AccountView), parameters);
            }
        }

        public AccountListViewModel(IRegionManager regionManager, IDialogService dialogService) : base(dialogService)
        {
            _regionManager = regionManager;
            InitList();
        }
        private async void InitList()
        {
            Categories = new ObservableCollection<AccountModel>();
            var json = await RestApiUtils.Instance.Get("api/user/users");
            var datas = Newtonsoft.Json.JsonConvert.DeserializeObject<List<AccountModel>>(json.Data);
            foreach (var elm in datas)
            {
                //Categories.Add(new AccountModel(elm.Id, elm.Name, elm.Description));
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
