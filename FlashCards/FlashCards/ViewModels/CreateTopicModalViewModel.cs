using System;
using System.Collections.Generic;
using System.Text;
using MvvmHelpers;
using FlashCards.Models;
using Xamarin.Forms;
using System.Windows.Input;

namespace FlashCards.ViewModels
{
    [QueryProperty("GroupId","GroupId")]
    public class CreateTopicModalViewModel : BaseViewModel
    {
        public CreateTopicModalViewModel()
        {
            SaveTopic = new Command(StoreTopic);

        }

        private string _groupId;
        private string _topicTitle;
        private string _topicDescription;

        public ICommand SaveTopic { get; }

        public string GroupId
        {
            get { return _groupId; }
            set
            {
                _groupId = value;
                OnPropertyChanged();
            }
        }

        public string TopicTitle
        {
            get { return _topicTitle; }
            set
            {
                _topicTitle = value;
                OnPropertyChanged();
            }
        }

        public string TopicDescription
        {
            get { return _topicDescription; }
            set
            {
                _topicDescription = value;
                OnPropertyChanged();
            }
        }

        private async void StoreTopic()
        {
            bool success = await App.Api.AddTopicAsync(GroupId, TopicTitle, TopicDescription);
            if (success) await AppShell.Current.GoToAsync("..");
        }
    }


}
