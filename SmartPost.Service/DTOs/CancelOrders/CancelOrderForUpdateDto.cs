namespace SmartPost.Service.DTOs.CancelOrders;
/// <summary>
/// Hamma DTO tugaganidan keyin mapperga bog'lash
/// </summary>
public record CancelOrderForUpdateDto
{
    public long SaleBy { get; set; }
    public string TransNo { get; set; }
    public string ProductName { get; set; }
    public string BarCode { get; set; }
    public string PCode { get; set; }
    public decimal Price { get; set; }
    public decimal TotalPrice { get; set; }
    public decimal Quantity { get; set; }
    public long CanceledBy { get; set; }
    public string Status { get; set; }
    public string Reason { get; set; }
    public string Action { get; set; }
    public DateTime ReturnDate { get; set; }
}
