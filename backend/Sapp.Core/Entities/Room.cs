using System;
using System.Collections.Generic;
using Sapp.Common.Enums;

namespace Sapp.Core.Entities
{
    public class Room : EntityBase
    {
        public string Name { get; set; }
        public RoomStatus Status { get; set; }
        public Guid? SelectedItemId { get; set; }

        public ICollection<RoomUser> RoomUsers { get; set; }
        public ICollection<Item> Items { get; set; }
        public ICollection<Vote> Votes { get; set; }
    }
}