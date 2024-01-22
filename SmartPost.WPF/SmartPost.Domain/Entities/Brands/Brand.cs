using SmartPost.Domain.Entities.InventoryLists;
using SmartPost.Domain.Entities.StokProducts;
using SmartPost.Domain.Entities.StorageProducts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartPost.Domain.Entities.Brands
{
    public class Brand : Auditable
    {
        public string Name { get; set; }
        public IEnumerable<StorageProduct> StorageProducts { get; set; }
        public IEnumerable<StokProduct> StokProducts { get; set; }
        public IEnumerable<InventoryList> InventoryLists { get; set; }
    }
}
