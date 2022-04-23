using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlashCards.ViewModels;
using FlashCards.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FlashCards.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Cards : ContentPage
    {
        private Stopwatch _stopwatch;
        public Cards()
        {
            InitializeComponent();
            _stopwatch = new Stopwatch();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            
            var viewModel = this.BindingContext as CardsViewModel;
            if(viewModel != null)
            {
                viewModel.GetCards();
            }
        }

        protected override void OnDisappearing()
        {
            if(_stopwatch != null && _stopwatch.IsRunning)
            {
                _stopwatch.Stop();
                _stopwatch.Reset();
            }
            base.OnDisappearing();
        }

        private async Task FlipCard(VisualElement from, VisualElement to)
        {
            await from.RotateYTo(-90,300, Easing.SpringIn);
            to.RotationY = 90;
            to.IsVisible = true;
            from.IsVisible = false;
            await to.RotateYTo(0,300, Easing.SpringOut);
        }

        private async void FlipToBack(object sender, EventArgs e)
        {
            var front = sender as Frame;
            var back = front.Parent.FindByName<Frame>("BackView");
            await FlipCard(front, back);

        }

        private async void FlipToFront(object sender, EventArgs e)
        {
            var back = sender as Frame;
            var front = back.Parent.FindByName<Frame>("FrontView");
            await FlipCard(back,front);

        }

        private async void AddCardToFavourites(object sender, EventArgs e)
        {
            var viewModel = this.BindingContext as CardsViewModel;
            var okay = await App.Api.AddCardToFavourite(viewModel.CurrentCard.Id);
            if (okay) await DisplayAlert("Success!", "Card has been added to favourites.", "Ok.");
        }

        private async void ItemChanged(object sender, CurrentItemChangedEventArgs e)
        {
            if(timerSwitch.IsToggled)
            {
                if (_stopwatch.IsRunning)
                {
                    _stopwatch.Stop();
                    var card = e.PreviousItem as Card;
                    await App.Api.UpdateCardAsync(card.Id, _stopwatch.Elapsed);
                    _stopwatch.Reset();

                }
                _stopwatch.Start();
            }
            
        }

        private async void OnToggled(object sender, ToggledEventArgs e)
        {
            var toggle = sender as Xamarin.Forms.Switch;
            if(toggle.IsToggled)
            {
                _stopwatch.Start();
            }
            else
            {
                var viewModel = this.BindingContext as CardsViewModel;
                var card = viewModel.CurrentCard;
                await App.Api.UpdateCardAsync(card.Id, _stopwatch.Elapsed);
                _stopwatch.Stop();
            }
                
        }
    }
}