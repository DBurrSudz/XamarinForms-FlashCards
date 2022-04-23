using System;
using System.Collections.Generic;
using System.Text;
using MvvmHelpers;
using Xamarin.Forms;
using FlashCards.Models;
using FlashCards.Views;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace FlashCards.ViewModels
{
    [QueryProperty("GroupId", "GroupId")]
    [QueryProperty("GroupTitle","GroupTitle")]
    public class TopicsViewModel : BaseViewModel
    {
        public TopicsViewModel()
        {
            CreateTopic = new Command(ShowCreateTopicModal);
            EditTopic = new Command<Topic>(ShowEditTopicModal);
            RemoveTopic = new Command<Topic>(DeleteTopic);
            GetCards = new Command<Topic>(ShowCards);
        }

        public ICommand CreateTopic { get; }
        public ICommand RemoveTopic { get; }
        public ICommand EditTopic { get; }
        public ICommand GetCards { get; }

        private ObservableCollection<Topic> _topics;
        public ObservableCollection<Topic> Topics
        {
            get { return _topics; }
            set
            {
                _topics = value;
                OnPropertyChanged();
            }
        }

        private string _groupId;
        private string _groupTitle;
        

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

        public async void GetTopics()
        {
            var topics = await App.Api.GetTopicsAsync(GroupId);
            Topics = new ObservableCollection<Topic>(topics);
        }

        private async void ShowCreateTopicModal()
        {
            await AppShell.Current.GoToAsync($"{nameof(CreateTopicModal)}?GroupId={GroupId}");
        }

        private async void ShowEditTopicModal(Topic topic)
        {
            await AppShell.Current.GoToAsync($"{nameof(EditTopicModal)}?TopicId={topic.Id}&TopicTitle={topic.Title}&TopicDescription={topic.Description}");
        }

        private async void ShowCards(Topic topic)
        {
            await AppShell.Current.GoToAsync($"{nameof(Cards)}?TopicId={topic.Id}&TopicTitle={topic.Title}&TotalCards={topic.Cards.Count}");
        }

        private async void DeleteTopic(Topic topic)
        {
            Topics.Remove(topic);
            await App.Api.DeleteTopicAsync(topic.Id);
        }
    }
}
