namespace SmartPost.Service.DTOs.Products;

public  class ProductForUpdateDto
{
    public long BrandId { get; set; }
    public decimal Price { get; set; }
    public long CategoryId { get; set; }
    public string ProductName { get; set; }
    public string BarCode { get; set; }
    public string PCode { get; set; }
    public decimal Quantity { get; set; }

}
