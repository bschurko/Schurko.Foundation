using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Schurko.Foundation.Identity.Auth
{
    public class AppUserManager : UserManager<AppUser, string>
    {
        public AppUserManager(IUserStore<AppUser, string> store)
            : base(store)
        {
              
        }
    }

}
