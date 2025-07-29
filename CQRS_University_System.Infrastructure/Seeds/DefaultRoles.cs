using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using CQRS_University_System.Domain.Enums;

namespace CQRS_University_System.Infrastructure.Seeds
{
    public static class DefaultRoles
    {
        public static async Task SeedAsync(RoleManager<IdentityRole> roleManager)
        {
            if (!await roleManager.Roles.AnyAsync())
            {
                await roleManager.CreateAsync(new IdentityRole { Name = Roles.Admin.ToString() });
                await roleManager.CreateAsync(new IdentityRole { Name = Roles.Student.ToString() });
            }
        }
    }
}
