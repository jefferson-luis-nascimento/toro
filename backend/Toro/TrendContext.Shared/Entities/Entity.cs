using System;

namespace TrendContext.Shared.Entities
{
    public abstract class Entity
    {
        public Guid Id { get; set; }
        public DateTime CreatedIn { get; set; }
        public DateTime? UpdatedIn { get; set; }
        public DateTime? DeletedIn { get; set; }
        public bool Deleted { get; set; }

        public Entity()
        {
            Id = Guid.NewGuid();
            CreatedIn = DateTime.Now;
        }
    }
}
