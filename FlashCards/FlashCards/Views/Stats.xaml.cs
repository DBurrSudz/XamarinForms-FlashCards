using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlashCards.Models;
using FlashCards.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FlashCards.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Stats : ContentPage
    {
        public Stats()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            var viewModel = this.BindingContext as StatsViewModel;
            if(viewModel != null)
            {
                viewModel.GetCards();
            }
        }
    }
}