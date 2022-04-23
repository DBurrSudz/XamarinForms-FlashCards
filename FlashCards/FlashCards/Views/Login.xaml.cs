using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FlashCards.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Login : ContentPage
    {
        private readonly IAuth _auth;
        public Login()
        {
            InitializeComponent();
            _auth = DependencyService.Get<IAuth>();
        }

        private async void ShowRegistration(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync($"{nameof(Registration)}");
        }

        private async void AuthenticateUser(object sender, EventArgs e)
        {
            bool valid = true;
            if (string.IsNullOrEmpty(Email.Text) || string.IsNullOrEmpty(Password.Text))
            {
                valid = false;
                await DisplayAlert("Error", "Fields cannot be empty.", "Ok");
            }

            if (valid)
            {
                var token = await _auth.Authenticate(Email.Text.Trim(), Password.Text.Trim());
                if (token != String.Empty)
                {
                    await Shell.Current.GoToAsync("//Home/Groups");
                }
                else
                {
                    await DisplayAlert("Error", "Invalid Credentials.", "Ok");
                }

            }
        }
    }
}