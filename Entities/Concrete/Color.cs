using Core.Entities;
using System.Collections.Generic;

namespace Entities.Concrete
{
    public class Color : BaseEntity
    {
        public string Name { get; set; }
        public ICollection<Product> Products { get; set; }
    }
}
