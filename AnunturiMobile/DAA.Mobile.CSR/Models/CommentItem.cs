using Xamarin.Forms;

namespace Anunturi.Mobile.Models
{
    public class CommentItem : BindableObject
    {
        private string _content;
        private string _author;
        private string _created;

        public string Content
        {
            get => _content;
            set
            {
                _content = value;
                OnPropertyChanged();
            }
        }

        public string Created
        {
            get => _created;
            set
            {
                _created = value;
                OnPropertyChanged();
            }
        }

        public string Author
        {
            get => _author;
            set
            {
                _author = value;
                OnPropertyChanged();
            }
        }

        public int Id { get; set; }
    }
}
