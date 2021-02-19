using System;

namespace Sapp.Common
{
    public class VoteDto
    {
        public Guid RoomId { get; set; }
        public Guid UserId { get; set; }
        public Guid ItemId { get; set; }
        public float? Vote { get; set; }
    }
}