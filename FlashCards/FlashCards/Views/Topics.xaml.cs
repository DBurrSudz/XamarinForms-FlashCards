using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using FlashCards.ViewModels;

namespace FlashCards.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Topics : ContentPage
    {
        public Topics()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            var viewModel = this.BindingContext as TopicsViewModel;

            if (viewModel != null)
            {
                viewModel.GetTopics();
            }
        }
    }
}