using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using Prism.Services.Dialogs;
using SaleManager.Wpf.Admin.Models;
using SaleManager.Wpf.Admin.Views;
using SaleManager.Wpf.Inflastructor;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace SaleManager.Wpf.Admin.ViewModels
{
    public class CategoryListViewModel : ViewModelBase, INavigationAware
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
                _regionManager.RequestNavigate("ContentMenuRegion", nameof(CategoryView), parameters);
            }
        }

        public CategoryListViewModel(IRegionManager regionManager, IDialogService dialogService) : base(dialogService)
        {
            _regionManager = regionManager;
            OnCreate = new DelegateCommand(Create);
            InitList();
        }
        private void Create()
        {
            Action a = () =>
            {
                _regionManager.RequestNavigate("ContentMenuRegion", nameof(CategoryView));
            };
            ExecuteAction(a);
        }
        private async void InitList()
        {
            Categories = new ObservableCollection<CategoryModel>();
            var datas = await RestApiUtils.Instance.Get<List<CategoryModel>>("api/category/getall");
            //var datas = Newtonsoft.Json.JsonConvert.DeserializeObject<List<CategoryModel>>(json.Data);
            foreach (var elm in datas)
            {
                Categories.Add(new CategoryModel(elm.Id, elm.Name, elm.Description));
            }
        }

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            InitList();
        }

        public bool IsNavigationTarget(NavigationContext navigationContext) => true;

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
            
        }
    }
}
