using CQRS_University_System.Domain.Identity;
using CQRS_University_System.Infrastructure.Seeds;
using Microsoft.AspNetCore.Identity;

namespace CQRS_University_System.API.Extensions
{
    public static class IdentitySeederExtension
    {
        public static async Task SeedIdentityAsync(this IServiceProvider services)
        {
            using var scope = services.CreateScope();

            var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();

            await DefaultRoles.SeedAsync(roleManager);
            await DefaultUsers.SeedAdminUserAsync(userManager);
        }
    }
}
