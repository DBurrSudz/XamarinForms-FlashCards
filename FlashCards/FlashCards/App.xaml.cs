using FlashCards.Views;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using FlashCards.Services;
using System.IO;

[assembly: ExportFont("Roboto-Regular.ttf",Alias = "Roboto")]
[assembly: ExportFont("Font Awesome 6 Free-Solid-900.otf",Alias = "FAS6")]
namespace FlashCards
{
    public partial class App : Application
    {
        private static ApiService _api;

        public static ApiService Api
        {
            get
            {
                if (_api == null)
                {
                    _api = new ApiService(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Flashcards.db3"));
                }
                return _api;
            }

        }

        public App()
        {
            InitializeComponent();
            MainPage = new AppShell();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
