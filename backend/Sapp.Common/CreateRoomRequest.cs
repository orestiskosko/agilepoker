using System;

namespace Sapp.Common
{
    public class CreateRoomRequest
    {
        public string Name { get; set; }
        public Guid OwnerId { get; set; }
    }
}