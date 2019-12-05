using Prism.Commands;
using Prism.Ioc;
using Prism.Mvvm;
using Prism.Regions;
using SaleManager.Wpf.Inflastructor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaleManager.Wpf.Admin.ViewModels.Menu
{
    public class HeaderViewModel : BindableBase
    {
        IRegionManager _regionManager;
        public string Username;
        public DelegateCommand OnAccountSetting { get; private set; }
        public DelegateCommand OnBack { get; private set; }
        public HeaderViewModel(IRegionManager regionManager)
        {
            _regionManager = regionManager;
            Username =  RestApiUtils.CurrentUser.User.GetName();
            OnBack = new DelegateCommand(Back, CanBack);
            OnBack.RaiseCanExecuteChanged();
        }

        public void Back()
        {
            _regionManager.Regions["ContentRegion"].NavigationService.Journal.GoBack();
            OnBack.RaiseCanExecuteChanged();
        }
        public bool CanBack()
        {
            return _regionManager.Regions["ContentRegion"].NavigationService.Journal.CanGoBack;
        }
    }
}
