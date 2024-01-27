using SmartPost.Domain.Commons;

namespace SmartPost.Domain.Entities.CancelOrders
{
    public class CancelOrder : Auditable
    {
        public string SaleBy { get; set; }
        public string TransNo { get; set; }
        public string ProductName { get; set; }
        public string Size { get; set; }
        public string BarCode { get; set; }
        public string PCode { get; set; }
        public decimal Price { get; set; }
        public decimal TotalPrice { get; set; }
        public decimal Quantity { get; set; }
        public string CanceledBy { get; set; }
        public string Reason { get; set; }
        public string Action { get; set; }
        public DateTime ReturnDate { get; set; }
    }
}
