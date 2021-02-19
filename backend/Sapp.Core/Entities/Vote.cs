using System;

namespace Sapp.Core.Entities
{
    public class Vote
    {
        public Guid UserId { get; set; }
        public User User { get; set; }

        public Guid RoomId { get; set; }
        public Room Room { get; set; }
        
        public Guid ItemId { get; set; }
        public Item Item { get; set; }

        public float? VoteData { get; set; }
    }
}