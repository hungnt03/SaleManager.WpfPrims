using Prism.Commands;
using Prism.Regions;
using Prism.Services.Dialogs;
using SaleManager.Wpf.Admin.Models;
using SaleManager.Wpf.Admin.Views;
using SaleManager.Wpf.Inflastructor;
using SaleManager.Wpf.Inflastructor.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace SaleManager.Wpf.Admin.ViewModels
{
    public class ProductListViewModel : ViewModelBase, INavigationAware
    {
        public ObservableCollection<ProductModel> _products;
        public ProductSearchModel _productSearch;
        private readonly IRegionManager _regionManager;
        IRegionNavigationJournal _journal;
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
        public ProductListViewModel(IRegionManager regionManager, IDialogService dialogService) : base(dialogService)
        {
            _regionManager = regionManager;
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
        private void Search()
        {

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
            foreach (var product in products)
                if (!string.IsNullOrEmpty(product.Img))
                    product.ImageBit = new BitmapImage(new Uri(product.Img));
            Products = products;
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
