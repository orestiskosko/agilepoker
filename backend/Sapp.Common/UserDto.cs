using System;

namespace Sapp.Common
{
    public class UserDto
    {
        public Guid Id { get; set; }
        public string Username { get; set; }
        public bool IsGuest { get; set; }
        public bool IsLeader { get; set; }
    }
}