using FlashCards.Models;
using System.Windows.Input;
using MvvmHelpers;
using Xamarin.Forms;

namespace FlashCards.ViewModels
{
    [QueryProperty("GroupId","GroupId")]
    [QueryProperty("GroupTitle","GroupTitle")]
    public class EditGroupModalViewModel : BaseViewModel
    {
        public EditGroupModalViewModel()
        {
            Cancel = new Command(NavigateBack);
            SaveGroup = new Command(UpdateGroup);
        }

        private string _groupId;
        private string _groupTitle;

        public ICommand SaveGroup { get; }

        public ICommand Cancel { get; }

        public string GroupId
        {
            get { return _groupId; }
            set
            {
                _groupId = value;
                OnPropertyChanged();
            }
        }

        public string GroupTitle
        {
            get { return _groupTitle; }
            set
            {
                _groupTitle = value;
                OnPropertyChanged();
            }
        }

        private async void NavigateBack()
        {
            await AppShell.Current.GoToAsync("..");
        }

        private async void UpdateGroup()
        {
            var okay = await App.Api.UpdateGroupAsync(GroupId,GroupTitle);
            if(okay) NavigateBack();
        }
    }
}
