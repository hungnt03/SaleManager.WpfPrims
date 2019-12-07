using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using Prism.Services.Dialogs;
using SaleManager.Wpf.Admin.Models;
using SaleManager.Wpf.Admin.Views;
using SaleManager.Wpf.Inflastructor;
using SaleManager.Wpf.Inflastructor.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaleManager.Wpf.Admin.ViewModels
{
    public class CustomerViewModel : ViewModelBase, INavigationAware
    {
        private readonly IRegionManager _regionManager;
        private readonly IDialogService _dialogService;
        private CustomerModel _customer = new CustomerModel();
        IRegionNavigationJournal _journal;
        public DelegateCommand OnSave { get; private set; }
        public DelegateCommand OnDelete { get; private set; }
        public bool _isEnable = false;
        private string DialogResult { set; get; }
        //public InteractionRequest<IConfirmation> ConfirmationRequest { get; set; }

        public CustomerModel Customer
        {
            get { return _customer; }
            set { SetProperty(ref _customer, value); }
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
        public CustomerViewModel(IRegionManager regionManager, IDialogService dialogService) : base(dialogService)
        {
            _regionManager = regionManager;
            _dialogService = dialogService;
            OnSave = new DelegateCommand(Save, CanSave)
                .ObservesProperty(() => this.Customer.Name);
            OnDelete = new DelegateCommand(Delete, CanDelete);
            //ConfirmationRequest = new InteractionRequest<IConfirmation>();
        }
        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            _journal = navigationContext.NavigationService.Journal;
            var customer = navigationContext.Parameters["customer"] as CustomerModel;
            if (customer != null)
            {
                Customer = customer;
                IsEnable = true;
            }
            else
            {
                Customer = new CustomerModel();
                IsEnable = false;
            }

        }
        private void Save()
        {
            if (!this.HasErrors)
            {
                Action a = async () =>
                {
                    var content = new Dictionary<string, object>{
                        { "Id", Customer.Id },
                        { "Name", Customer.Name },
                        { "Address", Customer.Address },
                        { "Contact", Customer.Contact },
                    };
                    bool isSuccess = false;
                    if (IsEnable)
                        isSuccess = await RestApiUtils.Instance.Post("api/customer/update", content);
                    else
                        isSuccess = await RestApiUtils.Instance.Post("api/customer/add", content);
                    if (isSuccess)
                    {
                        _journal.GoBack();
                    }
                };
                ExecuteAction(a);
            }
        }
        private void Delete()
        {
            Action a = async () =>
            {
                IDialogResult result = null;
                _dialogService.ShowDialog(nameof(ConfirmDialogView),
                    new DialogParameters
                    {{ "Content", "Xác nhận xoá bản ghi" }}, ret => result = ret);

                if (result.Result == ButtonResult.Yes)
                {
                    var content = new Dictionary<string, object> { { "id", Customer.Id } };
                    var isSuccess = await RestApiUtils.Instance.Post("api/customer/delete", content);
                    if (isSuccess)
                        _regionManager.RequestNavigate("ContentRegion", nameof(CustomerListView));
                }
            };
            ExecuteAction(a);
        }
        private bool CanSave()
        {
            //return true;
            return !this.HasErrors &&
                !string.IsNullOrWhiteSpace(Customer.Name);
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
