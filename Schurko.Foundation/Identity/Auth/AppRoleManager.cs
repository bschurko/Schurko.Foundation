using Microsoft.AspNet.Identity;
using Schurko.Foundation.Identity.Auth.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Schurko.Foundation.Identity.Auth
{
    public class AppRoleManager : RoleManager<ApplicationRole>
    {
        public AppRoleManager(IRoleStore<ApplicationRole> store) : base(store)
        {
            
        }
    }
}
