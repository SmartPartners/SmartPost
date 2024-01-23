using SmartPost.Domain.Entities.InventoryLists;
using SmartPost.Domain.Entities.StokProducts;
using SmartPost.Domain.Entities.StorageProducts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartPost.Domain.Entities.Categories
{
    public class Category : Auditable
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public IEnumerable<StorageProduct> StorageProducts { get; set; }
        public IEnumerable<StokProduct> StokProducts { get; set; }
        public IEnumerable<InventoryList> InventoryLists { get; set; }
    }
}
