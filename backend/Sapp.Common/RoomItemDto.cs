using System;
using Sapp.Common.Enums;

namespace Sapp.Common
{
    public class RoomItemDto
    {
        public Guid Id { get; set; }
        public object Data { get; set; }
        public RoomItemStatus Status { get; set; }
    }
}