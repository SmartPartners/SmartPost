namespace SmartPost.Service.DTOs.CancelOrders;
/// <summary>
/// Hamma DTO tugaganidan keyin mapperga bog'lash
/// </summary>
public record CancelOrderForResultDto
{
    public long Id { get; set; }
    public string ProductName { get; set; }
    public long SaleBy { get; set; }
    public string TransNo { get; set; }
    public string BarCode { get; set; }
    public string PCode { get; set; }
    public decimal Price { get; set; }
    public decimal TotalPrice { get; set; }
    public decimal Quantity { get; set; }
    public string CanceledBy { get; set; }
    public DateTime ReturnDate { get; set; }
    public string Reason { get; set; }
    public string Status { get; set; }
    public string Action { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}
