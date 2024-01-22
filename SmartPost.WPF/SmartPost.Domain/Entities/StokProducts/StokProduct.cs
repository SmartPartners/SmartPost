using SmartPost.Domain.Entities.Brands;
using SmartPost.Domain.Entities.Categories;
using SmartPost.Domain.Entities.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartPost.Domain.Entities.StokProducts
{
    public class StokProduct : Auditable
    {
        public Guid BrandId { get; set; }
        public Brand Brand { get; set; }

        public Guid CategoryId { get; set; }
        public Category Category { get; set; }

        public Guid UserId { get; set; }
        public User Users { get; set; }

        public string ProductName { get; set; }
        public string BarCode { get; set; }
        public int PCode { get; set; }
        public double Price { get; set; }
        public int Quantity { get; set; }
        public string Status { get; set; }
    }
}
