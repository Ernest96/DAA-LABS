using System.Threading.Tasks;

namespace Anunturi.Mobile.Services.Common.Alerts
{
    public interface IAlertService
    {
        Task ShowMessage(string caption, string message);

        Task ShowMessage(string caption, string message, string button);

        void DisplayErrorAlert(string message);

        Task DisplayInfoAlert(string message);
    }
}
