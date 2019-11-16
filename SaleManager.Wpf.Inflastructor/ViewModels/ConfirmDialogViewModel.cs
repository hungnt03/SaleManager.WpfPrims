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
        private string _content;

        public event Action<IDialogResult> RequestClose;

        public DelegateCommand<object> YesCommand { set; get; }
        public DelegateCommand<object> NoCommand { set; get; }
        public ConfirmDialogViewModel(IDialogService dialogService, IRegionManager regionManager)
        {
            _regionManager = regionManager;
            _dialogService = dialogService;

            YesCommand = new DelegateCommand<object>(Yes);
            NoCommand = new DelegateCommand<object>(No);
        }
        private void No(object button)
        {
            RequestClose(new DialogResult(button is ButtonResult buttonResult ? buttonResult : ButtonResult.No));
        }
        private void Yes(object button)
        {
            RequestClose(new DialogResult(button is ButtonResult buttonResult ? buttonResult : ButtonResult.Yes));
        }

        public bool CanCloseDialog() => true;

        public void OnDialogClosed()
        {
            
        }
        public void OnDialogOpened(IDialogParameters parameters)
        {
            if(parameters.ContainsKey("Content"))
                Content = parameters.GetValue<string>("Content");
        }
        public string Content
        {
            get { return _content; }
            set { SetProperty(ref _content, value); }
        }

        public string Title => "";
    }
}
