using SmartPost.DataAccess.Data;
using SmartPost.DataAccess.Interfaces.Users;
using SmartPost.Domain.Entities.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartPost.DataAccess.Repositories.Users
{
    public class UserRepository : Repository<User>, IUser
    {
        public UserRepository(AppDb app) : base(app)
        { }
    }
}
