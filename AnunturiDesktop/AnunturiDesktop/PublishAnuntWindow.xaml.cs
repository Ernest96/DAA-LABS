using AnunturiDesktop.DTO;
using AnunturiDesktop.Helpers;
using AnunturiDesktop.Services;
using AnunturiDesktop.Services.Auth;
using Refit;
using System.Threading.Tasks;
using System.Windows;

namespace AnunturiDesktop
{
    /// <summary>
    /// Interaction logic for MenuWindow.xaml
    /// </summary>
    public partial class PublishAnuntWindow : Window
    {
        //private readonly IAnuntApi _anuntApi;
        private readonly RoleService _roleService;
        private readonly IAnuntQueueApi _anuntQueueApi;

        public PublishAnuntWindow()
        {
            InitializeComponent();
            _roleService = new RoleService();
            //_anuntApi = RestService.For<IAnuntApi>(HttpClientFactory.Create(AppEnvironment.ApiBaseUrl));
            _anuntQueueApi = RestService.For<IAnuntQueueApi>(BrokerHttpClientFactory.Create(AppEnvironment.BrokerBaseUrl));
        }

        private async void SaveAnunt_Click(object sender, RoutedEventArgs e)
        {
            ShowLoading();
            await TaskHelper.TryWithErrorHandlingAsync(PublishAnuntAsync);
            HideLoading();
        }

        private async Task PublishAnuntAsync()
        {
            var html = HtmlEditor.ContentHtml;
            var subject = SubjectTextBox.Text;
            var role = RoleList.Text;

            if (!string.IsNullOrWhiteSpace(html) && !string.IsNullOrWhiteSpace(subject) && !string.IsNullOrWhiteSpace(role))
            {
                var anunt = new AnuntDto()
                {
                    Content = html,
                    Subject = subject,
                    Role = role
                };

                await _anuntQueueApi.Enqueue(anunt);

                this.Close();
            }
            else
            {
                MessageBox.Show("Introdu subiectul, rolul si continutul.");
            }
        }

        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var result = await TaskHelper.TryWithErrorHandlingAsync(_roleService.GetRoles);
            RoleList.ItemsSource = result.Value;
        }

        private void ShowLoading()
        {
            Loading.Visibility = Visibility.Visible;
            SaveAnunt.IsEnabled = false;
        }

        private void HideLoading()
        {
            Loading.Visibility = Visibility.Hidden;
            SaveAnunt.IsEnabled = true;
        }
    }
}
