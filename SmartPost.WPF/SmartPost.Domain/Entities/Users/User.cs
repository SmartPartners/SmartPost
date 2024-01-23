using SmartPost.Domain.Entities.Cards;
using SmartPost.Domain.Entities.RoleUsers;
using SmartPost.Domain.Entities.StokProducts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartPost.Domain.Entities.Users
{
    public class User : Auditable
    {
        public Guid RoleId { get; set; }
        public RoleUser RoleUsers {  get; set; }
        public string FirstName { get; set; } 
        public string LastName { get; set; } 
        public string PasswordHash { get; set; } 
        public string PasswordSalt { get; set; } 
        public string PhoneNumber { get; set; } 
        public string ImagePath { get; set; }

        public IEnumerable<StokProduct> StokProducts { get; set; }
        public IEnumerable<Card> Cards { get; set; }
    }
}
