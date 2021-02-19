using System;
using System.ComponentModel.DataAnnotations.Schema;
using Sapp.Common.Enums;

namespace Sapp.Core.Entities
{
    public class Item
    {
        public Guid Id { get; set; }
        
        [Column(TypeName = "jsonb")]
        public string Data { get; set; }
        
        public RoomItemStatus Status { get; set; }
        
        public Guid RoomId { get; set; }
        public Room Room { get; set; }
    }
}