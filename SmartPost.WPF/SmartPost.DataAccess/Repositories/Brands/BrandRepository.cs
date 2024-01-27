using SmartPost.DataAccess.Data;
using SmartPost.DataAccess.Interfaces.Barnds;
using SmartPost.Domain.Entities.Brands;

namespace SmartPost.DataAccess.Repositories.Brands;

public class BrandRepository : Repository<Brand>, IBrandRepository
{
    public BrandRepository(AppDbContext dbContext) : base(dbContext)
    {
    }
}
