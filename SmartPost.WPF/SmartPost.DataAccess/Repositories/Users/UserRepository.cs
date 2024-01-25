using SmartPost.DataAccess.Data;
using SmartPost.DataAccess.Interfaces.Users;
using SmartPost.Domain.Entities.Users;

namespace SmartPost.DataAccess.Repositories.Users
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(AppDbContext app) : base(app)
        { }
    }
}
