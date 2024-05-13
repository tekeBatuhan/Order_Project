using System;

namespace Core.Entities
{
    public class BaseEntity : BaseEntity<int> { }
    public class BaseEntity<T> : IEntity
    {
        public virtual int Id { get; set; }
        
    }
}
