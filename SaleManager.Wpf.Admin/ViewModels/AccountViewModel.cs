using MaterialDesignThemes.Wpf;
using Prism.Commands;
using Prism.Regions;
using Prism.Services.Dialogs;
using SaleManager.Wpf.Admin.Models;
using SaleManager.Wpf.Admin.Views;
using SaleManager.Wpf.Inflastructor;
using SaleManager.Wpf.Inflastructor.Models;
using SaleManager.Wpf.Inflastructor.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaleManager.Wpf.Admin.ViewModels
{
    public class AccountViewModel : ViewModelBase, INavigationAware
    {
        private readonly IRegionManager _regionManager;
        private readonly IDialogService _dialogService;
        IRegionNavigationJournal _journal;
        private AccountModel _account = new AccountModel();
        public DelegateCommand OnSave { get; private set; }
        public DelegateCommand GoBackCommand { get; set; }
        public bool _isEnable = false;
        private string DialogResult { set; get; }
        public AccountModel Account
        {
            get { return _account; }
            set { SetProperty(ref _account, value); }
        }
        public bool IsEnable
        {
            get { return _isEnable; }
            set{SetProperty(ref _isEnable, value);}
        }
        public AccountViewModel(IRegionManager regionManager, IDialogService dialogService) : base(dialogService)
        {
            _regionManager = regionManager;
            _dialogService = dialogService;
            OnSave = new DelegateCommand(Save, CanSave)
                .ObservesProperty(() => this.Account.FirstName)
                .ObservesProperty(() => this.Account.LastName);

            GoBackCommand = new DelegateCommand(() => _journal.GoBack(), () => _journal?.CanGoBack ?? false);
            //ConfirmationRequest = new InteractionRequest<IConfirmation>();
        }
        public async void OnNavigatedTo(NavigationContext navigationContext)
        {
            _journal = navigationContext.NavigationService.Journal;
            var account = navigationContext.Parameters["account"] as AccountModel;
            if (account != null)
            {
                Account = account;
                Account.Roles.Clear();
                var content = new Dictionary<string, object>{
                    { "Id", account.Id }
                };

                var roles = await RestApiUtils.Instance.Get<List<RoleModel>>("api/role/roles");
                //var roles = Newtonsoft.Json.JsonConvert.DeserializeObject<List<RoleModel>>(json.Data);
                if (roles != null && roles.Count > 0)
                {
                    foreach (var role in roles)
                        Account.Roles.Add(new Inflastructor.Models.CheckboxModel(role.Role));
                }

                var datas = await RestApiUtils.Instance.Post<List<string>>("api/user/getRoles", content);
                if (datas != null && datas.Count > 0)
                {
                    foreach (var role in datas)
                        foreach (var checkbox in Account.Roles)
                            if (role.Equals(checkbox.Label))
                                checkbox.IsChecked = true;
                }
            }
            else
                Account = new AccountModel();

            GoBackCommand.RaiseCanExecuteChanged();
        }
        private void Save()
        {
            if (!this.HasErrors)
            {
                Action a = async () =>
                {
                    var content = new Dictionary<string, object>{
                        { "Id", Account.Id },
                        { "FirstName", Account.FirstName },
                        { "LastName", Account.LastName },
                        { "Level", 1 },
                        { "IsEnable", Account.IsEnable },
                    };

                    bool isSuccses = await RestApiUtils.Instance.Post("api/user/update", content);
                    if (isSuccses)
                    {
                        var roles = new List<string>();
                        foreach (var currRole in Account.Roles)
                            if (currRole.IsChecked)
                                roles.Add(currRole.Label);
                        var role = new Dictionary<string, object>{
                            { "Id", Account.Id },
                            { "Roles", roles }
                        };
                        isSuccses = await RestApiUtils.Instance.Post("api/user/addRole", role);
                        if (isSuccses)
                            _journal.GoBack();
                    }
                };
                ExecuteAction(a);
            }
        }
        private bool CanSave()
        {
            //return true;
            return !this.HasErrors &&
                !string.IsNullOrWhiteSpace(Account.LastName) &&
                !string.IsNullOrWhiteSpace(Account.FirstName);
        }
        private bool CanDelete()
        {
            return IsEnable;
        }
        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            var account = navigationContext.Parameters["account"] as AccountModel;
            if (account != null)
                return Account != null && Account.Id == account.Id;
            else
                return true;
        }
        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
        }
    }
}
