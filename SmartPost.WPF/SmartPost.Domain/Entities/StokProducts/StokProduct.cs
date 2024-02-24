using SmartPost.Domain.Commons;
using SmartPost.Domain.Entities.Brands;
using SmartPost.Domain.Entities.Categories;
using SmartPost.Domain.Entities.Users;

namespace SmartPost.Domain.Entities.StokProducts
{
    public class StokProduct : Auditable
    {
        public long BrandId { get; set; }
        public Brand Brand { get; set; }

        public long CategoryId { get; set; }
        public Category Category { get; set; }

        public long UserId { get; set; }
        public User Users { get; set; }

        public string ProductName { get; set; }
        public string BarCode { get; set; }
        public string PCode { get; set; }
        public decimal Price { get; set; }
        public decimal? SalePrice { get; set; }
        public short? PercentageSalePrice { get; set; }
        public decimal Quantity { get; set; }
        public string Status { get; set; }
    }
}
