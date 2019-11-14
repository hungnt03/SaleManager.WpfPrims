using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using SaleManager.Wpf.Admin.Models;
using SaleManager.Wpf.Inflastructor;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace SaleManager.Wpf.Admin.ViewModels
{
    public class CategoryListViewModel : BindableBase
    {
        private ObservableCollection<CategoryModel> _categories;
        private CategoryModel _selectedItem;
        private readonly IRegionManager _regionManager;
        public DelegateCommand OnCreate { get; private set; }
        public ObservableCollection<CategoryModel> Categories
        {
            get { return _categories; }
            set
            {
                SetProperty(ref _categories, value);
            }
        }
        public CategoryModel SelectedItem
        {
            get { return _selectedItem; }
            set
            {
                SetProperty(ref _selectedItem, value);
                var parameters = new NavigationParameters();
                parameters.Add("category", value);
                _regionManager.RequestNavigate("ContentMenuRegion", "CategoryView",parameters);
            }
        }

        public CategoryListViewModel(IRegionManager regionManager)
        {
            _regionManager = regionManager;
            OnCreate = new DelegateCommand(Create);
            InitList();
        }
        private void Create()
        {
            _regionManager.RequestNavigate("ContentMenuRegion", "CategoryView");
        }
        private async void InitList()
        {
            if (Categories == null)
                Categories = new ObservableCollection<CategoryModel>();
            var json = await RestApiUtils.Instance.Get("api/category/getall");
            var datas = Newtonsoft.Json.JsonConvert.DeserializeObject<List<CategoryModel>>(json.Data);
            foreach (var elm in datas)
            {
                Categories.Add(new CategoryModel(elm.Id, elm.Name, elm.Description));
            }
        }
    }
}
