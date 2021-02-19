using System;

namespace Sapp.Core.Entities
{
    public class EntityBase
    {
        public Guid Id { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public DateTimeOffset UpdatedAt { get; set; }
    }
}