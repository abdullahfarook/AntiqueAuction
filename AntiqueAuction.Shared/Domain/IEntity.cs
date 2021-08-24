using System;

namespace AntiqueAuction.Shared.Domain
{
    // Base class which is shared by every domain model
    public abstract class Entity
    {
        public Guid Id { get; protected set; }
        public DateTime CreatedOn { get; protected set; }
        public DateTime UpdatedOn { get; protected set; }

        protected Entity()
        {
            Id = Guid.NewGuid();
        }
        public override bool Equals(object obj)
        {
            if (!(obj is Entity other))
                return false;

            if (ReferenceEquals(this, other))
                return true;

            if (GetRealType() != other.GetRealType())
                return false;

            return TypeSafeEqual(Id, other.Id);
        }

        protected bool TypeSafeEqual(Guid own, Guid other) => own == other;
        public static bool operator ==(Entity a, Entity b)
        {
            if (a is null && b is null)
                return true;

            if (a is null || b is null)
                return false;

            return a.Equals(b);
        }

        public static bool operator !=(Entity a, Entity b)
        {
            return !(a == b);
        }

        public override int GetHashCode()
        {
            return (GetRealType().ToString() + Id).GetHashCode();
        }

        private Type GetRealType()
        {
            var type = GetType();

            return type.ToString()
                .Contains("Castle.Proxies.") ?
                type.BaseType :
                type;
        }
    }
}