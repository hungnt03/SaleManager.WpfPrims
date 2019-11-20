using Prism.Interactivity.InteractionRequest;
using Prism.Services.Dialogs;
using SaleManager.Wpf.Inflastructor.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaleManager.Wpf.Inflastructor
{
    public abstract class ViewModelBase : AddBindableBase
    {
        private readonly IDialogService _dialogService;
        public ViewModelBase(IDialogService dialogService)
        {
            _dialogService = dialogService;
        }
        public void ExecuteAction(Action action)
        {
            try
            {
                action();
            }
            catch (Exception ex)
            {
                _dialogService.ShowDialog(nameof(NotificationDialogView),
                    new DialogParameters
                    {{ "Content", "Có lỗi xảy ra, vui lòng thử lại." + Environment.NewLine
                        + "Err:" + ex.Message}}
                    , ret => { });
            }
        }
    }
}
