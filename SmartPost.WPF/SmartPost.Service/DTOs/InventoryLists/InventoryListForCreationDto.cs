namespace SmartPost.Service.DTOs.InventoryLists;
/// <summary>
/// Hamma DTO tugaganidan keyin mapperga bog'lash
/// </summary>
public record InventoryListForCreationDto
{
    public long BrandId { get; set; }
    public long CategoryId { get; set; }
    public string ProductName { get; set; }
    public string BarCode { get; set; }
    public long PCode { get; set; }
    public decimal Price { get; set; }
    public decimal TotalPrice { get; set; }
    public decimal Quantity { get; set; }
}
