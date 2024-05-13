using Core.Entities;
using System.Collections.Generic;

namespace Entities.Concrete
{
    public class WareHouse : ProjectBaseEntity, IEntity
    {
        public string Name { get; set; }
        public virtual List<WareHouseProductMapping> WareHouseProductMappings { get; set; }

    }
}