using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartPost.Domain.Entities.CancelOrders
{
    public class CancelOrder : Auditable
    {
        public string SaleBy { get; set; }
        public string TransNo { get; set; }
        public string ProductName { get; set; }
        public string BarCode { get; set; }
        public int PCode { get; set; }
        public double Price { get; set; }
        public double TotalPrice { get; set; }
        public int Quantity { get; set; }
        public string CanceledBy { get; set; }
        public string Reason { get; set; }
        public string Action { get; set; }
        public DateTime ReturnDate { get; set; }
    }
}
