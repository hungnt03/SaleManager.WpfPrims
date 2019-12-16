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
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SaleManager.Wpf.Admin.ViewModels
{
    public class ProductViewModel : ViewModelBase, INavigationAware
    {
        private readonly IRegionManager _regionManager;
        private readonly IDialogService _dialogService;
        private ProductCreateModel _product = new ProductCreateModel();
        IRegionNavigationJournal _journal;
        public DelegateCommand OnSave { get; private set; }
        public DelegateCommand OnDelete { get; private set; }
        public DelegateCommand OnSelectImage { get; private set; }
        public bool _isEnable = false;
        private string DialogResult { set; get; }
        //public InteractionRequest<IConfirmation> ConfirmationRequest { get; set; }

        public ProductCreateModel Product
        {
            get { return _product; }
            set { SetProperty(ref _product, value); }
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
        public ProductViewModel(IRegionManager regionManager, IDialogService dialogService) : base(dialogService)
        {
            _regionManager = regionManager;
            _dialogService = dialogService;
            OnSave = new DelegateCommand(Save, CanSave)
                .ObservesProperty(() => this.Product.Name);
            OnDelete = new DelegateCommand(Delete, CanDelete);
            OnSelectImage = new DelegateCommand(OpenPopup);
            //ConfirmationRequest = new InteractionRequest<IConfirmation>();
        }
        public async void OnNavigatedTo(NavigationContext navigationContext)
        {
            _journal = navigationContext.NavigationService.Journal;
            var product = navigationContext.Parameters["product"] as ProductModel;
            var categories = await RestApiUtils.Instance.Get<List<CategoryModel>>("api/category/getall");
            var suppliers = await RestApiUtils.Instance.Get<List<SupplierModel>>("api/supplier/getall");
            if (product != null)
            {
                var content = new Dictionary<string, object>{
                    { "Barcode", product.Barcode },
                };
                var data = await RestApiUtils.Instance.Post<ProductCreateModel>("api/product/product", content);
                Product = data;
                Product.Categories = new ObservableCollection<CategoryModel>(categories); 
                Product.Suppliers = new ObservableCollection<SupplierModel>(suppliers);
                Product.CategorySelected = categories.Where(c => c.Id == product.CategoryId).FirstOrDefault();
                Product.SupplierSelected = suppliers.Where(c => c.Id == product.SupplierId).FirstOrDefault();
                IsEnable = true;
            }
            else
            {
                Product = new ProductCreateModel();
                Product.Categories = new ObservableCollection<CategoryModel>(categories);
                Product.Suppliers = new ObservableCollection<SupplierModel>(suppliers);
                IsEnable = false;
            }

        }
        public void OpenPopup()
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.DefaultExt = ".jpeg";
            fileDialog.Filter = "Image files (*.jpg, *.jpeg, *.jpe, *.jfif, *.png) | *.jpg; *.jpeg; *.jpe; *.jfif; *.png"; // Optional file extensions
            if (fileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                Product.Img = fileDialog.FileName;
            }
        }
        private void Save()
        {
            if (!this.HasErrors)
            {
                Action a = async () =>
                {
                    var content = new Dictionary<string, object>{
                        { "Barcode", Product.Barcode },
                        { "Name", Product.Name },
                        { "Price", Product.Price },
                        { "CategoryId", Product.CategoryId },
                        { "SupplierId", Product.SupplierId },
                        { "Pin", Product.Pin },
                        { "ExpirationDate", Product.ExpirationDate },
                        { "Unit", Product.Unit },
                        { "Price", Product.Price },
                        { "Img", Product.Img },
                    };
                    bool isSuccess = false;
                    if (IsEnable)
                        isSuccess = await RestApiUtils.Instance.Post("api/product/update", content);
                    else
                        isSuccess = await RestApiUtils.Instance.Post("api/product/add", content);
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
                    var content = new Dictionary<string, object> { { "Barcode", Product.Barcode } };
                    var isSuccess = await RestApiUtils.Instance.Post("api/product/delete", content);
                    if (isSuccess)
                        _regionManager.RequestNavigate(RegionNames.ContentRegion, nameof(ProductListView));
                }
            };
            ExecuteAction(a);
        }
        private bool CanSave()
        {
            //return true;
            return !this.HasErrors &&
                !string.IsNullOrWhiteSpace(Product.Name) &&
                Product.Price != 0;
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
