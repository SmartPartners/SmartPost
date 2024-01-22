using SmartPost.Domain.Entities.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartPost.Domain.Entities.Cards
{
    public class Card : Auditable
    {
        public Guid UserId { get; set; }
        public User Users { get; set; }

        public string TransNo { get; set; }
        public string ProductName { get; set; }
        public string BarCode { get; set; }
        public int PCode { get; set; }
        public double Price { get; set; }
        public double TotalPrice { get; set; }
        public double DiscPercent { get; set; }
        public int Quantity { get; set; }
        public string Status { get; set; }
    }
}
