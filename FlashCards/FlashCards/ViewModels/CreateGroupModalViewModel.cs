using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using MvvmHelpers;
using Xamarin.Forms;

namespace FlashCards.ViewModels
{
    public class CreateGroupModalViewModel : BaseViewModel
    {
        private readonly IAuth _auth;
        public CreateGroupModalViewModel()
        {
            _auth = DependencyService.Get<IAuth>();
            SaveGroup = new Command(StoreGroup);
        }

        public ICommand SaveGroup { get; }
        private string _groupName;
        public string GroupName
        {
            get { return _groupName; }
            set
            {
                if (_groupName == value) return;
                _groupName = value;
                OnPropertyChanged();
            }
        }

        private async void StoreGroup()
        {
            string token;
            if(_auth.Authenticated())
            {
                token = _auth.AuthenticatedToken();
                bool success = await App.Api.AddGroupAsync(GroupName, token);
                if (success)
                {
                    await AppShell.Current.GoToAsync("..");
                }
            }
        }
    }
}
