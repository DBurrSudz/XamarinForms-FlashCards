using System.Threading.Tasks;
using System.Windows.Input;
using MvvmHelpers;
using FlashCards.Views;
using FlashCards.Models;
using System.Collections.ObjectModel;
using Xamarin.Forms;
using System.Collections.Generic;

namespace FlashCards.ViewModels
{
    public class GroupViewModel : BaseViewModel
    {
        private ObservableCollection<Group> _groups;
        private readonly IAuth _auth;
        private string _query;

        public GroupViewModel()
        {
            CreateGroup = new Command(ShowCreateGroupModal);
            RemoveGroup = new Command<Group>(DeleteGroup);
            EditGroup = new Command<Group>(ShowEditGroupModal);
            ShowTopics = new Command<Group>(ShowTopicsPage);
            _auth = DependencyService.Get<IAuth>();
        }

        public ICommand CreateGroup { get; }
        public ICommand RemoveGroup { get; }
        public ICommand ShowTopics { get; }
        public ICommand EditGroup { get; }

        public ObservableCollection<Group> Groups
        {
            get
            {
                return _groups;
            }
            set
            {
                _groups = value;
                OnPropertyChanged();
            }
        }

        public string Query
        {
            get { return _query; }
            set
            {
                _query = value;
                OnPropertyChanged();
            }
        }

        private async void ShowCreateGroupModal()
        {
            await AppShell.Current.GoToAsync($"{nameof(CreateGroupModal)}");
        }

        private async void ShowEditGroupModal(Group group)
        {
            await AppShell.Current.GoToAsync($"{nameof(EditGroupModal)}?GroupId={group.Id}&GroupTitle={group.Title}");
        }

        private async void ShowTopicsPage(Group group)
        {
            await AppShell.Current.GoToAsync($"{nameof(Topics)}?GroupId={group.Id}&GroupTitle={group.Title}");
        }

        public async void GetGroups()
        {
            List<Group> groups = await App.Api.GetGroupsAsync(_auth.AuthenticatedToken());
            Groups = new ObservableCollection<Group>(groups);
        }

        private async void DeleteGroup(Group group)
        {
            Groups.Remove(group);
            await App.Api.DeleteGroupAsync(group.Id);
        }
    }
}
