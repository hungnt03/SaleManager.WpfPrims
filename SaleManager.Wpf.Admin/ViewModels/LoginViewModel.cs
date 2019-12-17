using Prism.Commands;
using Prism.Events;
using Prism.Regions;
using Prism.Services.Dialogs;
using SaleManager.Wpf.Admin.Views;
using SaleManager.Wpf.Admin.Views.Menu;
using SaleManager.Wpf.Inflastructor;
using SaleManager.Wpf.Inflastructor.Models;
using System;
using System.Collections.Generic;

namespace SaleManager.Wpf.Admin.ViewModels
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
        public LoginViewModel(IRegionManager regionManager, IDialogService dialogService, IEventAggregator ea) :base(dialogService)
        {
            _regionManager = regionManager;
            RestApiUtils.Instance._ea = ea;
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
            _regionManager.RequestNavigate(RegionNames.ContentRegion, nameof(AccountCreateView));
        }
        private void Login()
        {
            Action a = async () =>
            {
                var logged = await RestApiUtils.Instance.Login(Username, Password);
                if (logged)
                {
                    var response = await RestApiUtils.Instance.Post<ApplicationUserModel>("api/user/current", new Dictionary<string, object>());
                    RestApiUtils.CurrentUser = response;
                    _regionManager.RequestNavigate("ContentRegion", nameof(EmptyView));
                    _regionManager.RegisterViewWithRegion("HeaderRegion", typeof(HeaderView));
                    _regionManager.RequestNavigate("MenuRegion", nameof(MenuView));
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
