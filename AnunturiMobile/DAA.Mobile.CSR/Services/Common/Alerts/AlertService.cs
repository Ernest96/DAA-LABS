using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Anunturi.Mobile.Services.Common.Alerts
{
    public class AlertService : IAlertService
    {
        public Task ShowMessage(string caption, string message)
        {
            return ShowMessage(caption, message, "OK");
        }

        public Task ShowMessage(string caption, string message, string button)
        {
            return Application.Current.MainPage.DisplayAlert(caption, message, button);
        }

        public void DisplayErrorAlert(string message)
        {
            //We use Device.BeginInvoke to ensure that error alert will be shown (it will be fired on ui thread).
            Device.BeginInvokeOnMainThread(async () => { await ShowMessage("Error", message, "OK"); });
        }

        public Task DisplayInfoAlert(string message)
        {
            return ShowMessage("Info", message, "OK");
        }
    }
}
