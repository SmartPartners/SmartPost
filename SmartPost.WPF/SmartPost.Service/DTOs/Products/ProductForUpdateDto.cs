namespace SmartPost.Service.DTOs.Products;

public  class ProductForUpdateDto
{
    public long Id { get; set; }
    public long BrandId { get; set; }
    public decimal Price { get; set; }
    public long CategoryId { get; set; }
    public string ProductName { get; set; }
    
}
