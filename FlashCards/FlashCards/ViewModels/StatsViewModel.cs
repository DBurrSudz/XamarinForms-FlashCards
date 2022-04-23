using System;
using System.Collections.ObjectModel;
using FlashCards.Models;
using MvvmHelpers;
using Xamarin.Forms;
using System.Windows.Input;
using System.Text;

namespace FlashCards.ViewModels
{
    public class StatsViewModel : BaseViewModel
    {
        private ObservableCollection<Card> _cards;
        private bool _loaded;
        private readonly IAuth _auth;
        private Card _mostSpentCard;
        public StatsViewModel()
        {
            _auth = DependencyService.Get<IAuth>();
        }

        public Card MostSpentCard
        {
            get { return _mostSpentCard; }
            set
            {
                _mostSpentCard = value;
                OnPropertyChanged();
            }
        }

        public bool Loaded
        {
            get { return _loaded; }
            set { _loaded = value; OnPropertyChanged(); }
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
            string token;
            if(_auth.Authenticated())
            {
                token = _auth.AuthenticatedToken();
                var cards = await App.Api.GetStatsCardsAsync(token);
                if(cards == null || cards.Count == 0)
                {
                    Loaded = false;
                }
                else Loaded = true;

                if(cards.Count > 0) MostSpentCard = cards[0];
                cards.Remove(MostSpentCard);
                Cards = new ObservableCollection<Card>(cards);
            }
        }
    }
}
