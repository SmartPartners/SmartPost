using SmartPost.DataAccess.Data;
using SmartPost.DataAccess.Interfaces.Partners;
using SmartPost.Domain.Entities.Partners;

namespace SmartPost.DataAccess.Repositories.Partners;

public class PartnerProductRepository : Repository<PartnerProduct>, IPartnerProductRepository
{
    public PartnerProductRepository(AppDbContext dbContext) : base(dbContext)
    {
    }
}
