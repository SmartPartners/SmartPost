using SmartPost.Domain.Entities.InventoryLists;
using SmartPost.Domain.Entities.StokProducts;
using SmartPost.Domain.Entities.StorageProducts;

namespace SmartPost.Service.DTOs.Categories;

public record CategoryForResultDto
{
    public long Id { get; set; }
    public string Name { get; set; }

    public ICollection<StorageProduct> StorageProducts { get; set; }
    public ICollection<StokProduct> StokProducts { get; set; }
    public ICollection<InventoryList> InventoryLists { get; set; }
}
