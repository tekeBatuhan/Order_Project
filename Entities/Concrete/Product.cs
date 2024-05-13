using Core.Entities;
using Entities.Enums;
using System.Collections.Generic;

namespace Entities.Concrete
{
    public class Product : ProjectBaseEntity, IEntity
    {
        public string Name { get; set; }
        public Size Size  { get; set; }//boyut (S,M,L,XL,XXL..)
        public int ColorId { get; set; }
        public virtual Color Color { get; set; }
        public virtual List<WareHouseProductMapping> WareHouseProductMappings { get; set; }
        public virtual List<Order> Orders { get; set;}

    }
}
