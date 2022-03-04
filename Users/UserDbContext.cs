using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ShopApp.WebUI.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopApp.WebUI.Users
{
    public class UserDbContext :IdentityDbContext<User>
    {
        public UserDbContext(DbContextOptions<UserDbContext> options):base(options)
        {

        }
    }
}
