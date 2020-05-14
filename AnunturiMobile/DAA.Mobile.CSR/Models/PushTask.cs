using Xamarin.Forms;

namespace Anunturi.Mobile.Models
{
    public class PushTask : BindableObject
    {
        private string _flightNumber;
        private string _startsTime;

        public string FlightNumber
        {
            get => _flightNumber;
            set
            {
                _flightNumber = value;
                OnPropertyChanged();
            }
        }

        public string StartsTime
        {
            get => _startsTime;
            set
            {
                _startsTime = value;
                OnPropertyChanged();
            }
        }
    }
}
