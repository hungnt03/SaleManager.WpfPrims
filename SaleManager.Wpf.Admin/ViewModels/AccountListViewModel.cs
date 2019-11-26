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
        private readonly IRegionManager _regionManager;
        IRegionNavigationJournal _journal;
        public DelegateCommand<AccountModel> SelectedCommand { get; private set; }
        public ObservableCollection<AccountModel> Accounts
        {
            get { return _accounts; }
            set { SetProperty(ref _accounts, value); }
        }

        public AccountListViewModel(IRegionManager regionManager, IDialogService dialogService) : base(dialogService)
        {
            _regionManager = regionManager;
            SelectedCommand = new DelegateCommand<AccountModel>(AccountSelected);
            InitList();
        }
        private void AccountSelected(AccountModel account)
        {
            var parameters = new NavigationParameters();
            parameters.Add("account", account);
            if (account != null)
                _regionManager.RequestNavigate("ContentMenuRegion", nameof(AccountView), parameters);
        }
        private async void InitList()
        {
            Accounts = new ObservableCollection<AccountModel>();
            Accounts.Clear();
            var json = await RestApiUtils.Instance.Post("api/user/users", null);
            var datas = Newtonsoft.Json.JsonConvert.DeserializeObject<List<AccountModel>>(json.Data);
            foreach (var elm in datas)
            {
                Accounts.Add(new AccountModel()
                {
                    Id = elm.Id,
                    Email = elm.Email,
                    FirstName = elm.FirstName,
                    LastName = elm.LastName,
                    JoinDate = elm.JoinDate,
                    Level = elm.Level,
                    Username = elm.Username,
                    IsEnable = elm.IsEnable
                });
            }
        }

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            _journal = navigationContext.NavigationService.Journal;
        }

        public bool IsNavigationTarget(NavigationContext navigationContext) => true;

        public void OnNavigatedFrom(NavigationContext navigationContext){ }
    }
}
