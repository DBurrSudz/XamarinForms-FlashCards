using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using MvvmHelpers;
using System.Windows.Input;

namespace FlashCards.ViewModels
{
    [QueryProperty("TopicId","TopicId")]
    [QueryProperty("TopicTitle","TopicTitle")]
    [QueryProperty("TopicDescription","TopicDescription")]
    public class EditTopicModalViewModel : BaseViewModel
    {
        public EditTopicModalViewModel()
        {
            SaveTopic = new Command(UpdateTopic);
        }

        private string _topicId;
        private string _topicTitle;
        private string _topicDescription;

        public ICommand SaveTopic { get; }

        public string TopicId
        {
            get { return _topicId; }
            set
            {
                _topicId = value;
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

        private async void UpdateTopic()
        {
            bool okay = await App.Api.UpdateTopicAsync(TopicId, TopicTitle, TopicDescription);
            if (okay) await AppShell.Current.GoToAsync("..");
        }
    }
}
