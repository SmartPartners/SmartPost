using SmartPost.DataAccess.Data;
using SmartPost.DataAccess.Interfaces.Categories;
using SmartPost.Domain.Entities.Categories;

namespace SmartPost.DataAccess.Repositories.Categories;

public class CategoryRepository : Repository<Category>, ICategoryRepository
{
    public CategoryRepository(AppDbContext dbContext) : base(dbContext)
    {
    }
}
