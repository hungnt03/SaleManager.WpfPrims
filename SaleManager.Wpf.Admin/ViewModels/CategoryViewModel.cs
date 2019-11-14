using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using SaleManager.Wpf.Admin.Models;
using SaleManager.Wpf.Inflastructor;
using SaleManager.Wpf.Inflastructor.Models;
using System.Collections.Generic;
using Prism.Interactivity.InteractionRequest;

namespace SaleManager.Wpf.Admin.ViewModels
{
    public class CategoryViewModel : ViewModelBase, INavigationAware
    {
        private readonly IRegionManager _regionManager;
        private CategoryModel _category = new CategoryModel();
        public DelegateCommand OnSave { get; private set; }
        public DelegateCommand OnDelete { get; private set; }
        public bool _isEnable = false;
        
        public CategoryModel Category
        {
            get { return _category; }
            set { SetProperty(ref _category, value); }
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
        public CategoryViewModel(IRegionManager regionManager):base()
        {
            _regionManager = regionManager;
            OnSave = new DelegateCommand(Save);
            OnDelete = new DelegateCommand(Delete, CanDelete);
        }
        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            var category = navigationContext.Parameters["category"] as CategoryModel;
            if (category != null)
            {
                Category = category;
                IsEnable = true;
            }
            else
                Category = new CategoryModel();
        }

        private async void Save()
        {
            var content = new Dictionary<string, object>{
              { "Id", Category.Id },
              { "Name", Category.Name },
              { "Description", Category.Description },
            };
            ResponseData response;
            if(IsEnable)
                response = await RestApiUtils.Instance.Post("api/category/update", content);
            else
                response = await RestApiUtils.Instance.Post("api/category/add", content);
            if (response.IsSuccess())
                _regionManager.RequestNavigate("ContentMenuRegion", "CategoryListView");
        }
        private async void Delete()
        {
            var content = new Dictionary<string, object>{
              { "id", Category.Id }
            };
            var json = await RestApiUtils.Instance.Post("api/category/delete", content);
            if (json.IsSuccess())
                _regionManager.RequestNavigate("ContentMenuRegion", "CategoryListView");
        }

        private bool CanSave()
        {
            return true;
        }
        private bool CanDelete()
        {
            return IsEnable;
        }



        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            //var category = navigationContext.Parameters["category"] as CategoryModel;
            //if (category != null)
            //    return Category != null;
            //else
            //    return true;
            return true;
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {

        }
    }
}
