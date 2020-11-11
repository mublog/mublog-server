using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mublog.Server.Infrastructure.Data.Repositories
{
    class UserRepository : ARepository
    {
        public User GetByUsername(string name) => this.context.Users.Where(u => u.Username == name).FirstOrDefault();

    }
}
