
using AnunturiDesktop.Helpers;
using AnunturiDesktop.Services;
using AnunturiDesktop.Services.Auth;
using Refit;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace AnunturiDesktop
{
    /// <summary>
    /// Interaction logic for SmsWindow.xaml
    /// </summary>
    public partial class SmsWindow : Window
    {
        private readonly ISmsApi _smsApi;

        public SmsWindow()
        {
            InitializeComponent();
            _smsApi = RestService.For<ISmsApi>(HttpClientFactory.Create(AppEnvironment.ApiBaseUrl));

        }

        private async void Send_Click(object sender, RoutedEventArgs e)
        {
            ShowLoading();
            await TaskHelper.TryWithErrorHandlingAsync(SendSmsAsync);
            HideLoading();
        }

        private async Task SendSmsAsync()
        {
            var text = Content.Text;

            if (!string.IsNullOrWhiteSpace(text))
            {

                var errors = await _smsApi.SendSms(text);

                if(errors.Any())
                {
                    var errorsJoined = string.Join(", ", errors.ToArray());
                    MessageBox.Show($"Erori la transmitere: {errorsJoined}");
                }
                else
                {
                    MessageBox.Show("Mesaj transmis cu succes");
                    this.Close();
                }
            }
            else
            {
                MessageBox.Show("Introdu continutul.");
            }
        }

        private void ShowLoading()
        {
            Loading.Visibility = Visibility.Visible;
            Send.IsEnabled = false;
        }

        private void HideLoading()
        {
            Loading.Visibility = Visibility.Hidden;
            Send.IsEnabled = true;
        }
    }
}
