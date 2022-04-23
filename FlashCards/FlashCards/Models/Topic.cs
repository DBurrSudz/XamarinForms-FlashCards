using SQLite;
using System.Collections.Generic;
using SQLiteNetExtensions.Attributes;
namespace FlashCards.Models
{
    public class Topic
    {
        [PrimaryKey]
        public string Id { get; set; }
        public string Title { get; set; }

        public string Description { get; set; }

        [ForeignKey(typeof(Group))]
        public string GroupId { get; set; }
        public string LastOpened { get; set; }

        [ManyToOne(ReadOnly = true)]
        public Group Group { get; set; }

        [OneToMany(CascadeOperations = CascadeOperation.All)]
        public List<Card> Cards { get; set; }
    }
}
