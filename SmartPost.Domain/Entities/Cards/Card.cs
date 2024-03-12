using SmartPost.Domain.Commons;
using SmartPost.Domain.Entities.Users;

namespace SmartPost.Domain.Entities.Cards
{
    public class Card : Auditable
    {
        public long UserId { get; set; }
        public User Users { get; set; }

        public string TransNo { get; set; }
        public string ProductName { get; set; }
        public string BarCode { get; set; }
        public string PCode { get; set; }
        public decimal Price { get; set; }
        public decimal TotalPrice { get; set; }
        public decimal DiscPercent { get; set; }
        public decimal Quantity { get; set; }
        public string Status { get; set; }
    }
}
