using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using SaleManager.Wpf.Inflastructor;

namespace SaleManager.Wpf.ViewModels
{
    public class LoginViewModel : BindableBase
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
        public DelegateCommand<string> OnLogin { get; private set; }
        public LoginViewModel(IRegionManager regionManager)
        {
            _regionManager = regionManager;
            OnLogin = new DelegateCommand<string>(Login);

            //DUMMY DATA
            Username = "hungnt.hut56@gmail.com";
            Password = "Root@123";
        }
        private async void Login(string navigatePath)
        {
            var logged = await RestApiUtils.Instance.Login(Username, Password);
            if (logged && navigatePath != null)
                _regionManager.RequestNavigate("ContentRegion", navigatePath);
            else
                Messenger = "Tên đăng nhập hoặc mật khẩu không đúng!";
        }
    }
}
