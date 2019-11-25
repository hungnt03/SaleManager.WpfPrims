using MaterialDesignThemes.Wpf;
using Prism.Commands;
using Prism.Regions;
using Prism.Services.Dialogs;
using SaleManager.Wpf.Admin.Models;
using SaleManager.Wpf.Inflastructor;
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
        private AccountModel _account = new AccountModel();
        public DelegateCommand OnSave { get; private set; }
        public DelegateCommand OnDelete { get; private set; }
        public DelegateCommand OnDeleteRole { get; private set; }
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
            set
            {
                SetProperty(ref _isEnable, value);
                OnDelete.RaiseCanExecuteChanged();
            }
        }
        public AccountViewModel(IRegionManager regionManager, IDialogService dialogService) : base(dialogService)
        {
            _regionManager = regionManager;
            _dialogService = dialogService;
            OnSave = new DelegateCommand(Save, CanSave)
                .ObservesProperty(() => this.Account.FirstName)
                .ObservesProperty(() => this.Account.LastName);
            OnDelete = new DelegateCommand(Delete, CanDelete);
            OnDeleteRole = new DelegateCommand(DeleteRole);
            //ConfirmationRequest = new InteractionRequest<IConfirmation>();
        }
        public async void OnNavigatedTo(NavigationContext navigationContext)
        {
            var account = navigationContext.Parameters["account"] as AccountModel;
            if (account != null)
            {
                Account = account;
                IsEnable = true;
                var content = new Dictionary<string, object>{
                    { "Id", account.Id }
                };
                var json = await RestApiUtils.Instance.Post("api/user/getRoles", content);
                var datas = Newtonsoft.Json.JsonConvert.DeserializeObject<List<string>>(json.Data);
                if (datas != null && datas.Count > 0)
                    Account.CurrRoles = datas;
                else
                    Account.CurrRoles = new List<string>();

                json = await RestApiUtils.Instance.Get("api/role/roles");
                var roles = Newtonsoft.Json.JsonConvert.DeserializeObject<List<RoleModel>>(json.Data);
                if (roles != null && roles.Count > 0)
                {
                    var roleStrs = new List<string>();
                    foreach (var role in roles)
                        roleStrs.Add(role.Role);
                    Account.Roles = roleStrs;
                }
                else
                    Account.Roles = new List<string>();
            }
            else
                Account = new AccountModel();
        }
        private void DeleteRole()
        {
            var a = 1;
        }
        private void Save()
        {
            //if (!this.HasErrors)
            //{
            //    Action a = async () =>
            //    {
            //        var content = new Dictionary<string, object>{
            //      { "Id", Category.Id },
            //      { "Name", Category.Name },
            //      { "Description", Category.Description },
            //    };
            //        ResponseData response;
            //        if (IsEnable)
            //            response = await RestApiUtils.Instance.Post("api/category/update", content);
            //        else
            //            response = await RestApiUtils.Instance.Post("api/category/add", content);
            //        if (response.IsSuccess())
            //            _regionManager.RequestNavigate("ContentMenuRegion", nameof(CategoryListView));
            //    };
            //    ExecuteAction(a);
            //}
        }
        private void Delete()
        {
            //Action a = async () =>
            //{
            //    IDialogResult result = null;
            //    _dialogService.ShowDialog(nameof(ConfirmDialogView),
            //        new DialogParameters
            //        {{ "Content", "Xác nhận xoá bản ghi" }}, ret => result = ret);

            //    if (result.Result == ButtonResult.Yes)
            //    {
            //        var content = new Dictionary<string, object> { { "id", Category.Id } };
            //        var response = await RestApiUtils.Instance.Post("api/category/delete", content);
            //        if (response.IsSuccess())
            //            _regionManager.Regions["ContentMenuRegion"].NavigationService.Journal.GoBack();
            //        //_regionManager.RequestNavigate("ContentMenuRegion", nameof(CategoryListView));
            //    }
            //};
            //ExecuteAction(a);
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
        public bool IsNavigationTarget(NavigationContext navigationContext) => true;
        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
        }
    }
}
