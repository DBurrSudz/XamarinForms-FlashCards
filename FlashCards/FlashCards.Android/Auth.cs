using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using Xamarin.Forms;
using System.Threading.Tasks;
using Firebase.Auth;
using FlashCards.Droid;

[assembly: Dependency(typeof(Auth))] 
namespace FlashCards.Droid
{
    public class Auth : IAuth
    {
        public async Task<string> Authenticate(string email, string password)
        {
            try
            {
                var user = await Firebase.Auth.FirebaseAuth.Instance.SignInWithEmailAndPasswordAsync(email, password);
                var token = user.User.Uid;
                return token;
            }
            catch (FirebaseAuthInvalidUserException e)
            {
                e.PrintStackTrace();
                return String.Empty;
            }
            catch (FirebaseAuthInvalidCredentialsException e)
            {
                e.PrintStackTrace();
                return String.Empty;
            }

        }

        public bool Authenticated()
        {
            var user = Firebase.Auth.FirebaseAuth.Instance.CurrentUser;
            return user != null;
        }

        public async Task<string> Register(string email, string password)
        {
            try
            {
                var createdUser = await Firebase.Auth.FirebaseAuth.Instance.CreateUserWithEmailAndPasswordAsync(email, password);
                var token = createdUser.User.Uid;
                return token;
            }
            catch (FirebaseAuthInvalidUserException e)
            {
                e.PrintStackTrace();
                return String.Empty;
            }
            catch (FirebaseAuthInvalidCredentialsException e)
            {
                e.PrintStackTrace();
                return String.Empty;
            }
        }

        public string AuthenticatedToken()
        {
            if (Authenticated()) return Firebase.Auth.FirebaseAuth.Instance.CurrentUser.Uid;
            return null;
        }

        public bool SignOut()
        {
            try
            {
                Firebase.Auth.FirebaseAuth.Instance.SignOut();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}