using MvvmHelpers;
using Xamarin.Forms;
using System.Windows.Input;
using FlashCards.Models;
using System.Collections.ObjectModel;
using FlashCards.Views;
using Xamarin.Essentials;

namespace FlashCards.ViewModels
{
    [QueryProperty("TopicId","TopicId")]
    [QueryProperty("TopicTitle","TopicTitle")]
    [QueryProperty("TotalCards","TotalCards")]
    public class CardsViewModel : BaseViewModel
    {
        public CardsViewModel()
        {
            CreateCard = new Command(ShowCreateCardModal);
            HandlePositionChanged = new Command(PositionChanged);
            RemoveCard = new Command(DeleteCard);
            EditCard = new Command(ShowEditCardModal);
            TextToSpeechCardFront = new Command(ReadCardFront);
            TextToSpeechCardBack = new Command(ReadCardBack);
            
        }

        private ObservableCollection<Card> _cards;
        private string _topicId;
        private string _topicTitle;
        private Card _currentCard;
        private int _currentCardIndex;
        private int _totalCards;
        private float _progress;
        private bool _isTimerOn;
        
        public ICommand CreateCard { get; }

        public ICommand HandlePositionChanged { get; }

        public ICommand RemoveCard { get; }
        public ICommand EditCard { get; }

        public ICommand TextToSpeechCardFront { get; }
        public ICommand TextToSpeechCardBack { get; }

        public float Progress
        {
            get { return _progress; }
            set
            {
                _progress = value;
                OnPropertyChanged();
            }
        }

        public bool IsTimerOn
        {
            get { return _isTimerOn; }
            set
            {
                _isTimerOn = value;
            }
        }

        public int TotalCards
        {
            get { return _totalCards; }
            set
            {
                _totalCards = value;
                OnPropertyChanged();
            }
        }

        

        public Card CurrentCard
        {
            get { return _currentCard; }
            set
            {
                _currentCard = value;
                OnPropertyChanged();
            }
        }

        public int CurrentCardIndex
        {
            get { return _currentCardIndex; }
            set
            {
                if (TotalCards == 0) _currentCardIndex = value;
                else _currentCardIndex = value + 1;
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

        public string TopicTitle
        {
            get { return _topicTitle; }
            set
            {
                _topicTitle = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<Card> Cards
        {
            get { return _cards; }
            set
            {
                _cards = value;
                OnPropertyChanged();
            }
        }

        public async void GetCards()
        {
            var cards = await App.Api.GetCardsAsync(TopicId);
            Cards = new ObservableCollection<Card>(cards);
            TotalCards = Cards.Count;
        }

        private async void ShowCreateCardModal()
        {
            await AppShell.Current.GoToAsync($"{nameof(CreateCardModal)}?TopicId={TopicId}");
        }

        private async void ShowEditCardModal()
        {
            await AppShell.Current.GoToAsync($"{nameof(EditCardModal)}?CardId={CurrentCard.Id}&Heading={CurrentCard.Heading}&Content={CurrentCard.Content}");
        }

        private void PositionChanged()
        {
            Progress = (float)CurrentCardIndex / TotalCards;            
        }

        private async void DeleteCard()
        {
            if(TotalCards > 0)
            {
                Cards.Remove(CurrentCard);
                await App.Api.DeleteCardAsync(CurrentCard.Id);

            }
        }

        private void ReadCardFront()
        {
            TextToSpeech.SpeakAsync(CurrentCard.Heading, new SpeechOptions { Volume = 1.0f });
        }

        private void ReadCardBack()
        {
            TextToSpeech.SpeakAsync(CurrentCard.Content, new SpeechOptions { Volume = 1.0f});
        }
    }
}
