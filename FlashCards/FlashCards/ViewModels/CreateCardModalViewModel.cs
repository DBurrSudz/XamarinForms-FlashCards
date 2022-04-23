using MvvmHelpers;
using Xamarin.Forms;
using System.Windows.Input;

namespace FlashCards.ViewModels
{
    [QueryProperty("TopicId","TopicId")]
    public class CreateCardModalViewModel : BaseViewModel
    {
        private readonly IAuth _auth;
        public CreateCardModalViewModel()
        {
            _auth = DependencyService.Get<IAuth>();
            SaveCard = new Command(CreateCard);
        }

        private string _heading;
        private string _content;
        private string _topicId;
        public ICommand SaveCard { get; }

        public string Content
        {
            get { return _content; }
            set
            {
                _content = value;
                OnPropertyChanged();
            }
        }

        public string Heading
        {
            get { return _heading; }
            set
            {
                _heading = value;
                OnPropertyChanged();
            }
        }

        public string TopicId
        {
            get { return _topicId; }
            set
            {
                _topicId = value;
                OnPropertyChanged();
            }
        }

        private async void CreateCard()
        {
            string token;
            if(_auth.Authenticated())
            {
                token = _auth.AuthenticatedToken();
                var okay = await App.Api.AddCardAsync(TopicId, Heading, Content,token);
                if (okay) await AppShell.Current.GoToAsync("..");
            }
            
            
        }
    }
}
