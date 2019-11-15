using Prism.Commands;
using Prism.Regions;
using Prism.Services.Dialogs;
using Reactive.Bindings;
using Reactive.Bindings.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reactive.Disposables;
using System.Text;
using System.Threading.Tasks;

namespace SaleManager.Wpf.Inflastructor.ViewModels
{
    public class ConfirmDialogViewModel : AddBindableBase, IDialogAware
    {
        private IRegionNavigationService _regionNavigationService { get; set; }
        private IRegionManager _regionManager { get; }
        private IDialogService _dialogService { set; get; }
        private CompositeDisposable DisposeCollection = new CompositeDisposable();
        private string _parent;
        private string _content;

        public event Action<IDialogResult> RequestClose;

        public DelegateCommand YesCommand { set; get; }
        public DelegateCommand NoCommand { set; get; }
        public ConfirmDialogViewModel(IDialogService dialogService, IRegionManager regionManager)
        {
            _regionManager = regionManager;
            _dialogService = dialogService;

            YesCommand = new DelegateCommand(Yes);
            NoCommand = new DelegateCommand(No);
        }
        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            if (navigationContext.Parameters.ContainsKey("Content"))
                Content = navigationContext.Parameters.GetValue<string>("Content");
            if (navigationContext.Parameters.ContainsKey("Parent"))
                Parent = navigationContext.Parameters.GetValue<string>("Parent");
        }
        private void No()
        {
            var parameters = new DialogParameters { { "Result", "No" } };
            _regionManager.RequestNavigate("ContentMenuRegion", Parent, parameters);
        }
        private void Yes()
        {
            var parameters = new DialogParameters { { "Result", "Yes" } };
            _regionManager.RequestNavigate("ContentMenuRegion", Parent, parameters);
        }

        public bool CanCloseDialog() => true;

        public void OnDialogClosed()
        {
            
        }

        public void OnDialogOpened(IDialogParameters parameters)
        {
            
        }

        public string Content
        {
            get { return _content; }
            set { SetProperty(ref _content, value); }
        }
        public string Parent
        {
            get { return _parent; }
            set { SetProperty(ref _parent, value); }
        }

        public string Title => "";
    }
}
