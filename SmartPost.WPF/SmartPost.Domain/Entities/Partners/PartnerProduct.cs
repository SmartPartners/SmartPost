using SmartPost.Domain.Commons;
using SmartPost.Domain.Entities.Brands;
using SmartPost.Domain.Entities.Categories;

namespace SmartPost.Domain.Entities.Partners
{
    public class PartnerProduct : Auditable
    {
        public long PartnerId { get; set; }
        public Partner Partner { get; set; }

        public long BrandId { get; set; }
        public Brand Brand { get; set; }

        public long CategoryId { get; set; }
        public Category Category { get; set; }

        public string ProductName { get; set; }
        public string PCode { get; set; }
        public string BarCode { get; set; }
        public decimal Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal TotalPrice { get; set; }
        public string Status { get; set; }
        public decimal Paid { get; set; }
        public decimal Debt { get; set; }
    }
}
