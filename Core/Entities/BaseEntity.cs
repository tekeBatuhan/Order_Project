using System;

namespace Core.Entities
{
    public class BaseEntity : BaseEntity<int> { }
    public class BaseEntity<T> : IEntity
    {
        public virtual int Id { get; set; }
        public int CreatedUserId { get; set; }
        public DateTime CreatedDate { get; set; }
        public int LastUpdatedUserId { get; set; }
        public DateTime LastUpdatedDate { get; set; }
        public bool Status { get; set; }
        public bool isDeleted { get; set; }
    }
}
