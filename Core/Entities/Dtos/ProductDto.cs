
namespace Core.Entities.Dtos;

public class ProductDto : IEntity
{
    public int Id { get; set; }
    public int CreatedUserId { get; set; }
    public System.DateTime CreatedDate { get; set; }
    public int LastUpdatedUserId { get; set; }
    public System.DateTime LastUpdatedDate { get; set; }
    public bool Status { get; set; }
    public bool isDeleted { get; set; }
    public string Name { get; set; }
    public int ColorId { get; set; }
    public int Size { get; set; }
    public int WareHouseId { get; set; }
    public bool ReadyForSale { get; set; }
    public int Count { get; set; }
    public string WareHouseName { get; set; }

}

