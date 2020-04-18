using System;

namespace Notion.DAL.Entity.Abstract
{
    public abstract class BaseEntity<TId> : IEntity<TId>
    {
        public TId Id { get; set; }
        public DateTime CreatedAtUTC { get; set; }
        public DateTime? UpdatedAtUTC { get; set; }
    }
}