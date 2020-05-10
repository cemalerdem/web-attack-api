using System;

namespace Notion.DAL.Entity.Abstract
{
    public abstract class BaseEntity<TId> : IEntity<TId>
    {
        public TId Id { get; set; }
        public DateTime CreatedAtUTC { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAtUTC { get; set; }
    }
}