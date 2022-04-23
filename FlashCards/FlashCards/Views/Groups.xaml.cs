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
    public partial class Groups : ContentPage
    {
        public Groups()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            var viewModel = this.BindingContext as GroupViewModel;

            if(viewModel != null)
            {
                viewModel.GetGroups();
            }
        }
    }
}