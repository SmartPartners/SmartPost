using SmartPost.Domain.Commons;
using SmartPost.Domain.Entities.Cards;
using SmartPost.Domain.Entities.Partners;
using SmartPost.Domain.Entities.StokProducts;
using SmartPost.Domain.Enums;

namespace SmartPost.Domain.Entities.Users
{
    public class User : Auditable
    {
        public UserRole Role { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }
        public bool IsActive { get; set; }
        public string PasswordSalt { get; set; }
        public string PhoneNumber { get; set; }

        public IEnumerable<StokProduct> StokProducts { get; set; }
        public IEnumerable<Card> Cards { get; set; }
        public IEnumerable<PartnerProduct> PartnerProducts { get; set; }
    }
}
