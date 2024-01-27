using SmartPost.Domain.Entities.Brands;
using SmartPost.Domain.Entities.Categories;
using SmartPost.Service.DTOs.Brands;
using SmartPost.Service.DTOs.Categories;

namespace SmartPost.Service.DTOs.Products;

public  class ProductForResultDto
{
    public long Id { get; set; }
    public string PCode { get; set; }
    public long BrandId { get; set; }
    public string Size { get; set; }
    public decimal Price { get; set; }
    public string BarCode { get; set; }
    public long CategoryId { get; set; }
    public string ProductName { get; set; }
    public decimal Quantity { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }

   
}
