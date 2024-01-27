namespace SmartPost.Service.DTOs.Products;

public  class ProductForCreationDto
{
    public string PCode { get; set; }
    public long BrandId { get; set; }
    public decimal Price { get; set; }
    public string BarCode { get; set; }
    public long CategoryId { get; set; }
    public decimal Quantity { get; set; }
    public string ProductName { get; set; }

}
