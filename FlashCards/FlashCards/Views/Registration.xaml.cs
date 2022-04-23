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
    public partial class Registration : ContentPage
    {
        private readonly IAuth _auth;
        public Registration()
        {
            InitializeComponent();
            _auth = DependencyService.Get<IAuth>();
        }

        private async void RegisterUser(object sender, EventArgs e)
        {
            bool valid = true;
            if (string.IsNullOrEmpty(Email.Text) || string.IsNullOrEmpty(Password.Text) || string.IsNullOrEmpty(ConfirmPassword.Text))
            {
                valid = false;
                await DisplayAlert("Error", "Fields cannot be empty.", "Ok");
            }
            else
            {
                if (Password.Text != ConfirmPassword.Text)
                {
                    valid = false;
                    await DisplayAlert("Error", "Password and Confirm Password must be the same.", "Ok");
                }
            }

            if (valid)
            {
                var token = await _auth.Register(Email.Text.Trim(), Password.Text.Trim());
                if (token != String.Empty)
                {
                    await DisplayAlert("Success", "Account created.", "Ok");
                    var signOut = _auth.SignOut();
                    if (signOut) await Shell.Current.GoToAsync("//Login");
                }
                else
                {
                    await DisplayAlert("Error", "Account already exists.", "Ok");
                }

            }
            Email.Text = String.Empty;
            Password.Text = String.Empty;
            ConfirmPassword.Text = String.Empty;
            
        }
    }
}