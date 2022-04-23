using System;
using SQLite;
using SQLiteNetExtensions.Attributes;
namespace FlashCards.Models
{
    public class Card
    {
        [PrimaryKey]
        public string Id { get; set; }
        public string Heading { get; set; }
        public string Content { get; set; }

        public string UserId { get; set; }

        public bool IsFavourite { get; set; }

        public bool IsTimed { get; set; }

        [ForeignKey(typeof(Topic))]
        public string TopicId { get; set; }
        public TimeSpan Duration { get; set; }

        [ManyToOne(ReadOnly = true)]
        public Topic Topic { get; set; }
    }
}
