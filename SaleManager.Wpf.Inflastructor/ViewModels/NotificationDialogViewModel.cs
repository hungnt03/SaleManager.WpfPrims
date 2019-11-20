using Prism.Commands;
using Prism.Regions;
using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Disposables;
using System.Text;
using System.Threading.Tasks;

namespace SaleManager.Wpf.Inflastructor.ViewModels
{
    public class NotificationDialogViewModel : AddBindableBase, IDialogAware
    {
        private IRegionNavigationService _regionNavigationService { get; set; }
        private IRegionManager _regionManager { get; }
        private IDialogService _dialogService { set; get; }
        private CompositeDisposable DisposeCollection = new CompositeDisposable();
        private string _content;

        public event Action<IDialogResult> RequestClose;

        public DelegateCommand<object> OkCommand { set; get; }
        public NotificationDialogViewModel(IDialogService dialogService, IRegionManager regionManager)
        {
            _regionManager = regionManager;
            _dialogService = dialogService;

            OkCommand = new DelegateCommand<object>(Ok);
        }
        private void Ok(object button)
        {
            RequestClose(new DialogResult(button is ButtonResult buttonResult ? buttonResult : ButtonResult.OK));
        }

        public bool CanCloseDialog() => true;

        public void OnDialogClosed()
        {

        }
        public void OnDialogOpened(IDialogParameters parameters)
        {
            if (parameters.ContainsKey("Content"))
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
