using SmartPost.Domain.Entities.Users;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartPost.Domain.Entities.RoleUsers
{
    [Table("roleuser")]
    public class RoleUser : Auditable
    {
        public string Name { get; set; } 
        public IEnumerable<User> Users { get; set; }
    }
}
