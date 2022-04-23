using System;
using System.Collections.ObjectModel;
using System.Text;
using MvvmHelpers;
using Xamarin.Forms;
using System.Windows.Input;
using FlashCards.Models;

namespace FlashCards.ViewModels
{
    public class FavouritesViewModel : BaseViewModel
    {
        private ObservableCollection<Card> _cards;
        public ICommand RemoveCardFromFavourite { get; }
        private int _totalCards;
        public ObservableCollection<Card> Cards
        {
            get { return _cards; }
            set
            {
                _cards = value;
                OnPropertyChanged();
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
        private readonly IAuth _auth;
        public FavouritesViewModel()
        {
            _auth = DependencyService.Get<IAuth>();
            RemoveCardFromFavourite = new Command<Card>(RemoveCard);
        }


        public async void GetCards()
        {
            string token;
            if(_auth.Authenticated())
            {
                token = _auth.AuthenticatedToken();
                var cards = await App.Api.GetFavouriteCardsAsync(token);
                Cards = new ObservableCollection<Card>(cards);
                TotalCards = Cards.Count;
            }
            
        }

        private async void RemoveCard(Card card)
        {
            Cards.Remove(card);
            await App.Api.RemoveCardFromFavourites(card.Id);
            TotalCards = Cards.Count;
        }
    }
}
