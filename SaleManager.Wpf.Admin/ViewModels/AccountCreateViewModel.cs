using Prism.Commands;
using Prism.Events;
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
        IEventAggregator _ea;
        private AccountCreateModel _account = new AccountCreateModel();
        public DelegateCommand OnSave { get; private set; }
        public DelegateCommand OnBackCommand { get; }
        public AccountCreateModel Account
        {
            get { return _account; }
            set { SetProperty(ref _account, value); }
        }
        public AccountCreateViewModel(IRegionManager regionManager, IDialogService dialogService, 
            IEventAggregator ea) : base(dialogService)
        {
            _regionManager = regionManager;
            _ea = ea;
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

                    bool isSuccess = await RestApiUtils.Instance.Post("api/user/register", content);
                    if (isSuccess)
                    {
                        _ea.GetEvent<NotifSentEvent>().Publish("Tạo tài khoản thành công. " + Environment.NewLine + 
                            " Liên hệ với quản trị viên để kích hoạt.");
                        //_journal.GoBack();
                    } else
                        _ea.GetEvent<NotifSentEvent>().Publish("Có lỗi xảy ra, vui lòng thử lại");

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
            _journal = navigationContext.NavigationService.Journal;
            OnBackCommand.RaiseCanExecuteChanged();
        }

        public bool IsNavigationTarget(NavigationContext navigationContext) => true;

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
        }
    }
}