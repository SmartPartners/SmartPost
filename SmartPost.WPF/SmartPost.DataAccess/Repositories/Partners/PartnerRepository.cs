using SmartPost.DataAccess.Data;
using SmartPost.DataAccess.Interfaces.Partners;
using SmartPost.Domain.Entities.Partners;

namespace SmartPost.DataAccess.Repositories.Partners;

public class PartnerRepository : Repository<Partner>, IPartnerRepository
{
    public PartnerRepository(AppDbContext dbContext) : base(dbContext)
    {
    }
}
