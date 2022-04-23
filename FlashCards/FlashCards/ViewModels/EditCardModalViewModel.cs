using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using MvvmHelpers;
using System.Windows.Input;

namespace FlashCards.ViewModels
{
    [QueryProperty("CardId","CardId")]
    [QueryProperty("Heading","Heading")]
    [QueryProperty("Content","Content")]
    public class EditCardModalViewModel : BaseViewModel
    {
        public EditCardModalViewModel()
        {
            SaveCard = new Command(UpdateCard);
        }

        public ICommand SaveCard { get; }

        private string _heading;
        private string _content;
        private string _cardId;

        public string Heading
        {
            get { return _heading; }
            set
            {
                _heading = value;
                OnPropertyChanged();
            }
        }

        public string Content
        {
            get { return _content; }
            set
            {
                _content = value;
                OnPropertyChanged();
            }
        }

        public string CardId
        {
            get { return _cardId; }
            set
            {
                _cardId = value;
            }
        }

        private async void UpdateCard()
        {
            var okay = await App.Api.UpdateCardAsync(CardId,Heading,Content);
            if (okay) await AppShell.Current.GoToAsync("..");
        }
    }
}
