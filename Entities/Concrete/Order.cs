using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
    public class Order : ProjectBaseEntity, IEntity
    {
        public int Quantity { get; set; }
        public int ProductId { get; set; }
        public int CustomerId { get; set; }
        public virtual Customer Customer { get; set; }
        public virtual Product Product { get; set; }


    }
}
