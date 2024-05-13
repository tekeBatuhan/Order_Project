using Core.Entities;
using System.Collections.Generic;

namespace Entities.Concrete
{
    public class Color : ProjectBaseEntity, IEntity
    {
        public string Name { get; set; }
        public virtual List<Product> Products { get; set; }
    }
}
