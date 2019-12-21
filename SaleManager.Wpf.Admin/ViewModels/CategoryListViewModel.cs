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
using System.ComponentModel;

namespace SaleManager.Wpf.Admin.ViewModels
{
    public class CategoryListViewModel : ViewModelBase, INavigationAware
    {
        public ObservableCollection<CategoryModel> _categories;
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
            var datas = await RestApiUtils.Instance.Get<List<CategoryModel>>("api/category/getall");
            Categories = new ObservableCollection<CategoryModel>(datas);
        }

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            _journal = navigationContext.NavigationService.Journal;
            var category = navigationContext.Parameters["categoryAdd"] as CategoryModel;
            if (category != null)
                Categories.Add(category);
            category = navigationContext.Parameters["categoryDelete"] as CategoryModel;
            if (category != null)
            {
                foreach(var elm in Categories)
                    if (elm.Id == category.Id)
                        Categories.Remove(elm);
            }
        }

        public bool IsNavigationTarget(NavigationContext navigationContext) => true;

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
        }
    }
}
