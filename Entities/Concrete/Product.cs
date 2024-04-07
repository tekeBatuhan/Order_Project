using Core.Entities;

namespace Entities.Concrete
{
    public class Product : BaseEntity
    {
        public string Name { get; set; }
        public string Size { get; set; }//boyut (S,M,L,XL,XXL..)
        public int ColorId { get; set; }
        public virtual Color Color { get; set; }
        public int WareHouseId { get; set; }
        public virtual WareHouse WareHouse { get; set; }
    }
}
