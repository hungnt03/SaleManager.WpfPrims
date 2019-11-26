using Prism.Commands;
using Prism.Regions;
using Prism.Services.Dialogs;
using SaleManager.Wpf.Admin.Models;
using SaleManager.Wpf.Inflastructor;
using SaleManager.Wpf.Inflastructor.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaleManager.Wpf.Admin.ViewModels
{
    public class AccountCreateViewModel : ViewModelBase, INavigationAware
    {
        private readonly IRegionManager _regionManager;
        IRegionNavigationJournal _journal;
        private AccountCreateModel _account = new AccountCreateModel();
        public DelegateCommand OnSave { get; private set; }
        public DelegateCommand OnBackCommand { get; set; }
        public AccountCreateModel Account
        {
            get { return _account; }
            set { SetProperty(ref _account, value); }
        }
        public AccountCreateViewModel(IRegionManager regionManager, IDialogService dialogService) : base(dialogService)
        {
            _regionManager = regionManager;
            OnSave = new DelegateCommand(Save, CanSave)
                .ObservesProperty(() => this.Account.FirstName)
                .ObservesProperty(() => this.Account.LastName);

            OnBackCommand = new DelegateCommand(() => _journal.GoBack(), () => _journal?.CanGoBack ?? false);
        }
        private void Save()
        {
            if (!this.HasErrors)
            {
                Action a = async () =>
                {
                    var content = new Dictionary<string, object>{
                        { "Username", Account.Username },
                        { "Password", Account.Password },
                        { "ConfirmPassword", Account.ConfirmPassword },
                        { "Email", Account.Email },
                        { "FirstName", Account.FirstName },
                        { "LastName", Account.LastName },
                        { "Level", 1 },
                        { "IsEnable", false },
                    };

                    ResponseData response = await RestApiUtils.Instance.Post("api/user/register", content);
                    if (response.IsSuccess())
                    {
                        var roles = new List<string>();
                        //foreach (var currRole in Account.Roles)
                        //    if (currRole.IsChecked)
                        //        roles.Add(currRole.Label);
                        var role = new Dictionary<string, object>{
                            //{ "Id", Account.Id },
                            //{ "Roles", roles }
                        };
                        response = await RestApiUtils.Instance.Post("api/user/addRole", role);

                        if (response.IsSuccess())
                            _journal.GoBack();
                    }
                };
                ExecuteAction(a);
            }
        }
        private bool CanSave()
        {
            return true;
        }

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            throw new NotImplementedException();
        }

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            throw new NotImplementedException();
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
            throw new NotImplementedException();
        }
    }
}