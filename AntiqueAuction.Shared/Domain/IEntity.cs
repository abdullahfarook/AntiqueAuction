using System;

namespace AntiqueAuction.Shared.Domain
{
    // Base class which is shared by every domain model
    public class Entity
    {
        public Guid Id { get; protected set; }
        public DateTime CreatedOn { get; protected set; }
        public DateTime UpdatedOn { get; protected set; }
        public Entity()
        {
            Id = Guid.NewGuid();
        }
    }
}