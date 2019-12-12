using Prism.Commands;
using Prism.Regions;
using Prism.Services.Dialogs;
using Prism.Events;
using SaleManager.Wpf.Admin.Models;
using SaleManager.Wpf.Admin.Views;
using SaleManager.Wpf.Inflastructor;
using SaleManager.Wpf.Inflastructor.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Media.Imaging;

namespace SaleManager.Wpf.Admin.ViewModels
{
    public class ProductListViewModel : ViewModelBase, INavigationAware
    {
        public ObservableCollection<ProductModel> _products;
        public ProductSearchModel _productSearch;
        private readonly IRegionManager _regionManager;
        IRegionNavigationJournal _journal;
        Prism.Events.IEventAggregator _ea;
        public DelegateCommand OnCreate { get; private set; }
        public DelegateCommand OnSearch { get; private set; }
        public DelegateCommand<ProductModel> SelectedCommand { get; private set; }
        public ProductSearchModel ProductSearch
        {
            get { return _productSearch; }
            set
            {
                SetProperty(ref _productSearch, value);
            }
        }
        public ObservableCollection<ProductModel> Products
        {
            get { return _products; }
            set
            {
                SetProperty(ref _products, value);
            }
        }
        public ProductListViewModel(IRegionManager regionManager, IDialogService dialogService, IEventAggregator ea) : base(dialogService)
        {
            _regionManager = regionManager;
            _ea = ea;
            OnCreate = new DelegateCommand(Create);
            SelectedCommand = new DelegateCommand<ProductModel>(ProductSelected);
            OnSearch = new DelegateCommand(Search);
            //InitList();
        }
        private void ProductSelected(ProductModel product)
        {
            var parameters = new NavigationParameters();
            parameters.Add("product", product);
            if (product != null)
                _regionManager.RequestNavigate(RegionNames.ContentRegion, nameof(ProductView), parameters);
        }
        private async void Search()
        {
            var content = new Dictionary<string, object>{
                { "NameOrBarcode", ProductSearch.BarcodeOrName },
                { "Category", (ProductSearch.CategorySelected != null) ? ProductSearch.CategorySelected.Id : -1 },
                { "Supplier", (ProductSearch.SupplierSelected != null) ? ProductSearch.SupplierSelected.Id : -1 },
            };
            var datas = await RestApiUtils.Instance.Post<List<ProductModel>>("api/product/search",content);
            Products = new ObservableCollection<ProductModel>(datas);
            if(datas.Count == 0)
                _ea.GetEvent<NotifSentEvent>().Publish("Không tìm được dữ liệu thoả mãn, xin thử lại");
        }
        private void Create()
        {
            Action a = () =>
            {
                _regionManager.RequestNavigate(RegionNames.ContentRegion, nameof(ProductView));
            };
            ExecuteAction(a);
        }
        private async void InitList()
        {
            var datas = await RestApiUtils.Instance.Get<List<ProductModel>>("api/product/getall");
            var products = new ObservableCollection<ProductModel>(datas);
            Products = products;

            var productSearch = new ProductSearchModel();
            var categories = await RestApiUtils.Instance.Get<List<CategoryModel>>("api/category/getall");
            productSearch.Categories = new ObservableCollection<CategoryModel>(categories);
            var suppliers = await RestApiUtils.Instance.Get<List<SupplierModel>>("api/supplier/getall");
            productSearch.Suppliers = new ObservableCollection<SupplierModel>(suppliers);
            ProductSearch = productSearch;
        }

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            _journal = navigationContext.NavigationService.Journal;
            InitList();
        }

        public bool IsNavigationTarget(NavigationContext navigationContext) => true;

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
        }
    }
}
