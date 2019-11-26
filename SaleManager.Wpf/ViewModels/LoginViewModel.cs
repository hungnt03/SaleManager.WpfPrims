using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using Prism.Services.Dialogs;
using SaleManager.Wpf.Admin.Views;
using SaleManager.Wpf.Inflastructor;
using SaleManager.Wpf.Models;
using SaleManager.Wpf.Views.Menu;
using System;
using System.Collections.Generic;

namespace SaleManager.Wpf.ViewModels
{
    public class LoginViewModel : ViewModelBase
    {
        private string _username;
        private string _password;
        private string _mess;
        private readonly IRegionManager _regionManager;
        public string Password
        {
            get { return _password; }
            set { SetProperty(ref _password, value); }
        }
        public string Username
        {
            get { return _username; }
            set { SetProperty(ref _username, value); }
        }
        public string Messenger
        {
            get { return _mess; }
            set { SetProperty(ref _mess, value); }
        }
        public DelegateCommand OnLogin { get; private set; }
        public DelegateCommand OnCreateAccount { get; private set; }
        public DelegateCommand OnFogotPassword { get; private set; }
        public LoginViewModel(IRegionManager regionManager, IDialogService dialogService):base(dialogService)
        {
            _regionManager = regionManager;
            OnLogin = new DelegateCommand(Login, CanLogin)
                .ObservesProperty(() => this.Username)
                .ObservesProperty(() => this.Password);
            OnCreateAccount = new DelegateCommand(CreateAccount);
            OnFogotPassword = new DelegateCommand(ForgotPassword);

            //DUMMY DATA
            Username = "hungnt03";
            Password = "Root@123";
        }
        private void ForgotPassword()
        {

        }
        private void CreateAccount()
        {
            _regionManager.RequestNavigate("ContentMenuRegion", nameof(AccountCreateView));
        }
        private void Login()
        {
            Action a = async () =>
            {
                var logged = await RestApiUtils.Instance.Login(Username, Password);
                if (logged)
                {
                    var response = await RestApiUtils.Instance.Post("api/user/current", new Dictionary<string, object>());
                    MainWindowViewModel.CurrentUser = Newtonsoft.Json.JsonConvert.DeserializeObject<ApplicationUserModel>(response.Data); ;
                    _regionManager.RequestNavigate("ContentRegion", nameof(MenuView));
                    _regionManager.RegisterViewWithRegion("HeaderRegion", typeof(HeaderView));
                }
                else
                    Messenger = "Tên đăng nhập hoặc mật khẩu không đúng!";
            };
            ExecuteAction(a);
        }
        private bool CanLogin()
        {
            return !string.IsNullOrWhiteSpace(Username) && !string.IsNullOrWhiteSpace(Password);
        }
    }
}
