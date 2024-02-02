using SmartPost.Domain.Commons;
using SmartPost.Domain.Entities.InventoryLists;
using SmartPost.Domain.Entities.Partners;
using SmartPost.Domain.Entities.StokProducts;
using SmartPost.Domain.Entities.StorageProducts;

namespace SmartPost.Domain.Entities.Categories
{
    public class Category : Auditable
    {
        public string Name { get; set; }

        public IEnumerable<Product> Products { get; set; }
        public IEnumerable<StokProduct> StokProducts { get; set; }
        public IEnumerable<InventoryList> InventoryLists { get; set; }
        public IEnumerable<PartnerProduct> PartnersProducts { get; set; }
    }
}
