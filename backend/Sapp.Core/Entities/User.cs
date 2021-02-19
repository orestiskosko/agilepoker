using System.Collections.Generic;

namespace Sapp.Core.Entities
{
    public class User : EntityBase
    {
        public string Username { get; set; }
        public string PasswordHash { get; set; }
        public bool IsGuest { get; set; }
        
        public ICollection<RoomUser> RoomUsers { get; set; }
    }
}