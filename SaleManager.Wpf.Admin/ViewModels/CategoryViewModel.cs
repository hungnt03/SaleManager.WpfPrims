using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using SaleManager.Wpf.Admin.Models;
using SaleManager.Wpf.Inflastructor;
using SaleManager.Wpf.Inflastructor.Models;
using System.Collections.Generic;
using Prism.Interactivity.InteractionRequest;
using System;
using MaterialDesignThemes.Wpf;
using Prism.Services.Dialogs;
using SaleManager.Wpf.Inflastructor.Views;
using SaleManager.Wpf.Admin.Views;

namespace SaleManager.Wpf.Admin.ViewModels
{
    public class CategoryViewModel : ViewModelBase, INavigationAware
    {
        private readonly IRegionManager _regionManager;
        private readonly IDialogService _dialogService;
        private CategoryModel _category = new CategoryModel();
        public DelegateCommand OnSave { get; private set; }
        public DelegateCommand OnDelete { get; private set; }
        public bool _isEnable = false;
        private string DialogResult { set; get; }
        //public InteractionRequest<IConfirmation> ConfirmationRequest { get; set; }

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
        public CategoryViewModel(IRegionManager regionManager, IDialogService dialogService) : base()
        {
            _regionManager = regionManager;
            _dialogService = dialogService;
            OnSave = new DelegateCommand(Save, CanSave);
            OnDelete = new DelegateCommand(Delete, CanDelete);
            //ConfirmationRequest = new InteractionRequest<IConfirmation>();
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
            if (!this.HasErrors)
            {
                var content = new Dictionary<string, object>{
                  { "Id", Category.Id },
                  { "Name", Category.Name },
                  { "Description", Category.Description },
                };
                ResponseData response;
                if (IsEnable)
                    response = await RestApiUtils.Instance.Post("api/category/update", content);
                else
                    response = await RestApiUtils.Instance.Post("api/category/add", content);
                if (response.IsSuccess())
                    _regionManager.RequestNavigate("ContentMenuRegion", nameof(CategoryListView));
            }
        }
        private async void Delete()
        {
            IDialogResult result = null;
            _dialogService.ShowDialog(nameof(ConfirmDialogView),
                new DialogParameters
                {
                    { "Content", "Xác nhận xoá bản ghi" },
                    { "Parent", "CategoryView" }
                }
                , ret => result = ret);

            if (result.Result == ButtonResult.Yes)
            {
                var content = new Dictionary<string, object>{{ "id", Category.Id }};
                var response = await RestApiUtils.Instance.Post("api/category/delete", content);
                if (response.IsSuccess())
                    _regionManager.RequestNavigate("ContentMenuRegion", nameof(CategoryListView));
            }
        }
        private bool CanSave()
        {
            return true;
                //!this.HasErrors && !string.IsNullOrWhiteSpace(Category.Name);
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
            var a = 1;
        }
    }
}
