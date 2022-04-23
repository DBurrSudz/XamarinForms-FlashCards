using System;
using System.Threading.Tasks;
using FlashCards.Models;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using SQLite;
using SQLiteNetExtensionsAsync.Extensions;

namespace FlashCards.Services
{
    public sealed class ApiService
    {
        private readonly SQLiteAsyncConnection _connection;
        public ApiService(string path) 
        {
            _connection = new SQLiteAsyncConnection(path);
            /**
              _connection.DropTableAsync<Card>();
            
            _connection.DropTableAsync<Topic>();
            _connection.DropTableAsync<Group>();
            **/
            _connection.CreateTableAsync<Group>();
            _connection.CreateTableAsync<Topic>();
            _connection.CreateTableAsync<Card>();
        }

        private string GenerateId()
        {
            return Guid.NewGuid().ToString();
        }

        public async Task<bool> AddGroupAsync(string title, string userId)
        {
            try
            {
                var group = new Group { Id = GenerateId(), Title = title, UserId = userId };
                await _connection.InsertAsync(group);
                return true;

            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> UpdateGroupAsync(string groupId, string title)
        {
            try
            {
                var updatedGroup = await _connection.GetAsync<Group>(groupId);
                updatedGroup.Title = title;
                await _connection.UpdateAsync(updatedGroup);
                return true;

            }
            catch (Exception)
            {
                return false;
            }
        }

        public Task<List<Group>> GetGroupsAsync(string userId)
        {
            try
            {
                var groups = _connection.GetAllWithChildrenAsync<Group>(filter: group => group.UserId.Equals(userId));
                /**
                var groups = _connection.Table<Group>()
                                              .Where(group => group.UserId.Equals(userId))
                                              .ToListAsync();
                **/
                return groups;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public Task<List<Topic>> GetTopicsAsync(string groupId)
        {
            try
            {
                var topics = _connection.GetAllWithChildrenAsync<Topic>(filter: topic => topic.GroupId.Equals(groupId));
   
                //var topics = _connection.Table<Topic>()
                                                    //   .Where(topic => topic.GroupId.Equals(groupId))
                                                     //  .ToListAsync();
                return topics;
            }
            catch (Exception)
            {

                return null;
            }
        }

        public async Task<bool> DeleteGroupAsync(string groupId)
        {
            try
            {
                var group = await _connection.GetAsync<Group>(groupId);
                await _connection.DeleteAsync(group, recursive: true);
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }

        public async Task<bool> AddTopicAsync(string groupId, string title, string description)
        {
            try
            {
                var group = await _connection.GetWithChildrenAsync<Group>(groupId);
                var topic = new Topic { Id = GenerateId(), Title = title, LastOpened = DateTime.Now.ToString(), Description = description, GroupId = groupId };
                await _connection.InsertAsync(topic);
                if(group.Topics == null)
                {
                    group.Topics = new List<Topic> { topic };
                }
                else
                {
                    group.Topics.Add(topic);
                }
                await _connection.UpdateWithChildrenAsync(group);
                return true;

            }
            catch (Exception)
            {
                return false;
            }

        }

        public async Task<bool> UpdateTopicAsync(string topicId, string title, string description)
        {
            try
            {
                var updatedTopic = await _connection.GetAsync<Topic>(topicId);
                updatedTopic.Title = title;
                updatedTopic.Description = description;
                await _connection.UpdateAsync(updatedTopic);
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }

        public async Task<bool> DeleteTopicAsync(string topicId)
        {
            try
            {
                var topic = await _connection.GetAsync<Topic>(topicId);
                await _connection.DeleteAsync(topic, recursive: true);
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }

        public Task<List<Card>> GetCardsAsync(string topicId)
        {
            try
            {
                var cards = _connection.Table<Card>()
                                                     .Where(card => card.TopicId.Equals(topicId))
                                                     .OrderByDescending(card => card.Duration)
                                                     .ToListAsync();
                return cards;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public Task<List<Card>> GetStatsCardsAsync(string userId)
        {
            try
            {
                var cards = _connection.Table<Card>()
                                                    .Where(card => card.UserId.Equals(userId) && card.IsTimed)
                                                    .OrderByDescending(card => card.Duration)
                                                    .Take(5)
                                                    .ToListAsync();
                return cards;
            }
            catch (Exception)
            {

                return null;
            }
            
        }

        public async Task<bool> AddCardAsync(string topicId, string heading, string content, string userId)
        {
            try
            {
                var topic = await _connection.GetWithChildrenAsync<Topic>(topicId);
                var card = new Card { Id = GenerateId(), Heading = heading, Content = content, TopicId = topicId, Duration = TimeSpan.Zero, IsFavourite = false, IsTimed = false, UserId = userId };
                await _connection.InsertAsync(card);
                if(topic.Cards == null)
                {
                    topic.Cards = new List<Card> { card };
                }
                else
                {
                    topic.Cards.Add(card);
                }

                await _connection.UpdateWithChildrenAsync(topic);
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }

        public async Task<bool> UpdateCardAsync(string cardId, string heading, string content)
        {
            try
            {
                var updatedCard = await _connection.GetAsync<Card>(cardId);
                updatedCard.Heading = heading;
                updatedCard.Content = content;
                await _connection.UpdateAsync(updatedCard);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> UpdateCardAsync(string cardId, TimeSpan duration)
        {
            try
            {
                var updatedCard = await _connection.GetAsync<Card>(cardId);
                updatedCard.Duration = updatedCard.Duration.Add(duration);
                updatedCard.IsTimed = true;
                await _connection.UpdateAsync(updatedCard);
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }

        public async Task<bool> DeleteCardAsync(string cardId)
        {
            try
            {
                var deletedCard = await _connection.GetAsync<Card>(cardId);
                await _connection.DeleteAsync(deletedCard);
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }

        public async Task<bool> AddCardToFavourite(string cardId)
        {
            try
            {
                var card = await _connection.GetAsync<Card>(cardId);
                card.IsFavourite = true;
                await _connection.UpdateAsync(card);
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }

        public async Task<bool> RemoveCardFromFavourites(string cardId)
        {
            try
            {
                var card = await _connection.GetAsync<Card>(cardId);
                card.IsFavourite = false;
                await _connection.UpdateAsync(card);
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }

        public Task<List<Card>> GetFavouriteCardsAsync(string userId)
        {
            try
            {
                var cards = _connection.Table<Card>()
                                                     .Where(card => card.UserId.Equals(userId) && card.IsFavourite)
                                                     .ToListAsync();
                return cards;
            }
            catch (Exception)
            {
                return null;
            }
        }

    }
}
