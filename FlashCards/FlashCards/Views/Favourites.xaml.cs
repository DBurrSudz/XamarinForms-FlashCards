using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlashCards.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FlashCards.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Favourites : ContentPage
    {
        public Favourites()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            var viewModel = this.BindingContext as FavouritesViewModel;
            if (viewModel != null)
            {
                viewModel.GetCards();
            }
        }

        private async void CardRemoved(object sender, EventArgs e)
        {
            await DisplayAlert("Success!", "Card removed from favourites.", "Ok.");
        }
    }
}