﻿using System;

namespace Sapp.Core.Entities
{
    public class RoomUser
    {
        public Guid RoomId { get; set; }
        public Room Room { get; set; }
        
        public Guid UserId { get; set; }
        public User User { get; set; }
        
        public bool IsLeader { get; set; }
    }
}