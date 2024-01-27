using SmartPost.Service.DTOs.InventoryLists;
using SmartPost.Service.DTOs.Products;
using SmartPost.Service.DTOs.StockProducts;

namespace SmartPost.Service.DTOs.Brands;

public class BrandForResultDto
{
    public long Id { get; set; }
    public string Name { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }

    public ICollection<ProductForResultDto> Products { get; set; }
    public ICollection<StockProductsForResultDto> StokProducts { get; set; }
    public ICollection<InventoryListForResultDto> InventoryLists { get; set; }
}
