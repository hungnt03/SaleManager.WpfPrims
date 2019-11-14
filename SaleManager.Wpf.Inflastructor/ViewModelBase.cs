using Prism.Interactivity.InteractionRequest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaleManager.Wpf.Inflastructor
{
    public abstract class ViewModelBase : AddBindableBase
    {
        [Obsolete]
        public InteractionRequest<IConfirmation> ConfirmationRequest { get; set; }
        [Obsolete]
        public InteractionRequest<INotification> NotificationRequest { get; set; }

        [Obsolete]
        public ViewModelBase()
        {
            ConfirmationRequest = new InteractionRequest<IConfirmation>();
            NotificationRequest = new InteractionRequest<INotification>();
        }

        public void ConfirmDeleteDialog()
        {
            ConfirmationRequest.Raise(new Confirmation { Title = "Xác nhận", Content = "Bạn có muốn xoá bản ghi?" }, OnDialogClosed);
        }

        public virtual void OnDialogClosed(IConfirmation confirmation)
        {
            if (confirmation.Confirmed)
            {
                //perform the confirmed action...
            }
            else
            {

            }
        }
    }
}
