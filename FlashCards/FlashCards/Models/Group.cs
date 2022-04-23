using System.Collections.Generic;
using SQLite;
using SQLiteNetExtensions.Attributes;
using System.Collections.ObjectModel;

namespace FlashCards.Models
{
    public class Group
    {
        [PrimaryKey]
        public string Id { get; set; }
        public string Title { get; set; }
        public string UserId { get; set; }
        
        [OneToMany(CascadeOperations = CascadeOperation.All)]
        public List<Topic> Topics { get; set; }
    }
}
