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
        IRegionNavigationJournal _journal;
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
        public CategoryViewModel(IRegionManager regionManager, IDialogService dialogService) : base(dialogService)
        {
            _regionManager = regionManager;
            _dialogService = dialogService;
            OnSave = new DelegateCommand(Save, CanSave)
                .ObservesProperty(() => this.Category.Name)
                .ObservesProperty(() => this.Category.Description);
            OnDelete = new DelegateCommand(Delete, CanDelete);
            //ConfirmationRequest = new InteractionRequest<IConfirmation>();
        }
        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            _journal = navigationContext.NavigationService.Journal;
            var category = navigationContext.Parameters["category"] as CategoryModel;
            if (category != null)
            {
                Category = category;
                IsEnable = true;
            }
            else
            {
                Category = new CategoryModel();
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
                        { "Id", Category.Id },
                        { "Name", Category.Name },
                        { "Description", Category.Description },
                    };
                    //Update
                    if (IsEnable && await RestApiUtils.Instance.Post("api/category/update", content))
                    {
                        _journal.GoBack();
                    }
                    //Add
                    var addedData = await RestApiUtils.Instance.Post<CategoryModel>("api/category/add", content);
                    if (addedData != null)
                    {
                        var parameters = new NavigationParameters();
                        parameters.Add("categoryAdd", addedData);
                        _regionManager.RequestNavigate(RegionNames.ContentRegion, nameof(CategoryListView), parameters);
                    }
                    //bool isSuccess = false;
                    //if (IsEnable)
                    //    isSuccess = await RestApiUtils.Instance.Post("api/category/update", content);
                    //else
                    //    isSuccess = await RestApiUtils.Instance.Post("api/category/add", content);
                    //if (isSuccess) 
                    //{
                    //    if(IsEnable)
                    //        _journal.GoBack();
                    //    else
                    //    {
                    //        var parameters = new NavigationParameters();
                    //        parameters.Add("category", content);
                    //    }

                    //}
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
                    var content = new Dictionary<string, object> { { "id", Category.Id } };
                    var isSuccess = await RestApiUtils.Instance.Post("api/category/delete", content);
                    if (isSuccess)
                    {
                        var parameters = new NavigationParameters();
                        parameters.Add("categoryDelete", Category);
                        _regionManager.RequestNavigate(RegionNames.ContentRegion, nameof(CategoryListView), parameters);
                    }
                }
            };
            ExecuteAction(a);
        }
        private bool CanSave()
        {
            //return true;
            return !this.HasErrors && 
                !string.IsNullOrWhiteSpace(Category.Name) && 
                !string.IsNullOrWhiteSpace(Category.Description); ;
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
