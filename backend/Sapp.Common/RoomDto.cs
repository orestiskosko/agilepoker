using System;
using System.Collections.Generic;
using Sapp.Common.Enums;

namespace Sapp.Common
{
    public class RoomDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid LeaderId { get; set; }
        public Guid? SelectedItemId { get; set; }
        public IEnumerable<UserDto> Participants { get; set; }
        public IEnumerable<RoomItemDto> Items { get; set; }
        public IEnumerable<VoteDto> Votes { get; set; }
        public RoomStatus Status { get; set; }
    }
}