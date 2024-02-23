namespace SmartPost.Service.DTOs.StockProducts;

public class StockProductForCreationDto
{
    public long BrandId { get; set; }
    public long CategoryId { get; set; }
    public long UserId { get; set; }
    public string ProductName { get; set; }
    public string BarCode { get; set; }
    public string PCode { get; set; }
    public decimal Price { get; set; }
    public decimal? SalePrice { get; set; }
    public short? PercentageSalePrice { get; set; }
    public decimal Quantity { get; set; }

}
