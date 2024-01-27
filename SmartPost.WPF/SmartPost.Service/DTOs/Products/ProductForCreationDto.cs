namespace SmartPost.Service.DTOs.Products;

public class ProductForCreationDto
{
    public long BrandId { get; set; }
    public string Size { get; set; }
    public decimal Price { get; set; }
    public string PCode { get; set; }
    public string BarCode { get; set; }
    public long CategoryId { get; set; }
    public decimal Quantity { get; set; }
    public string ProductName { get; set; }

}
