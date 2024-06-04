using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
    public class WareHouseProductMapping : ProjectBaseEntity, IEntity
    {
        public int ProductId { get; set; }
        public int WareHouseId { get; set; }
        public int Count { get; set; }
        public bool ReadyForSale { get; set; }
        public virtual WareHouse WareHouse { get; set; }
        public virtual Product Product { get; set; }
    }
}
