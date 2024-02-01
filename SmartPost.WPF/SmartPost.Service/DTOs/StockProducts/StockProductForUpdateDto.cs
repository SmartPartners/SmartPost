namespace SmartPost.Service.DTOs.StockProducts;

public class StockProductForUpdateDto
{
    public long UserId { get; set; }
    public long BrandId { get; set; }
    public decimal Price { get; set; }
    public long CategoryId { get; set; }
    public string ProductName { get; set; }
    public string BarCode { get; set; }
    public string PCode { get; set; }
    public decimal Quantity { get; set; }

}
