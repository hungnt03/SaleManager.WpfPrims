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

namespace SaleManager.Wpf.Admin.ViewModels
{
    public class CategoryListViewModel : ViewModelBase, INavigationAware
    {
        private ObservableCollection<CategoryModel> _categories;
        private readonly IRegionManager _regionManager;
        IRegionNavigationJournal _journal;
        public DelegateCommand OnCreate { get; private set; }
        public DelegateCommand<CategoryModel> SelectedCommand { get; private set; }
        public ObservableCollection<CategoryModel> Categories
        {
            get { return _categories; }
            set
            {
                SetProperty(ref _categories, value);
            }
        }

        public CategoryListViewModel(IRegionManager regionManager, IDialogService dialogService) : base(dialogService)
        {
            _regionManager = regionManager;
            OnCreate = new DelegateCommand(Create);
            SelectedCommand = new DelegateCommand<CategoryModel>(CategorySelected);
            InitList();
        }
        private void CategorySelected(CategoryModel category)
        {
            var parameters = new NavigationParameters();
            parameters.Add("category", category);
            if (category != null)
                _regionManager.RequestNavigate(RegionNames.ContentRegion, nameof(CategoryView), parameters);
        }
        private void Create()
        {
            Action a = () =>
            {
                _regionManager.RequestNavigate(RegionNames.ContentRegion, nameof(CategoryView));
            };
            ExecuteAction(a);
        }
        private async void InitList()
        {
            if (Categories == null)
                Categories = new ObservableCollection<CategoryModel>();
            var datas = await RestApiUtils.Instance.Get<List<CategoryModel>>("api/category/getall");
            foreach (var elm in datas)
            {
                Categories.Add(new CategoryModel(elm.Id, elm.Name, elm.Description));
            }
        }

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            _journal = navigationContext.NavigationService.Journal;
            //InitList();
        }

        public bool IsNavigationTarget(NavigationContext navigationContext) => true;

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
        }
    }
}
