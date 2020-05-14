using Xamarin.Forms;

namespace Anunturi.Mobile.Models
{
    public class PushInfo : BindableObject
    {
        private string _subject;

        public string Subject
        {
            get => _subject;
            set
            {
                _subject = value;
                OnPropertyChanged();
            }
        }

        public int Id { get; set; }
    }
}
