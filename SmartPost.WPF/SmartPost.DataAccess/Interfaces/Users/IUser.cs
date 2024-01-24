using SmartPost.Domain.Entities.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartPost.DataAccess.Interfaces.Users
{
    public interface IUser : IRepository<User>
    { }
}
