using Microsoft.AspNetCore.Identity;

namespace Weather.Api.Models
{
    public class RoleConfiguration
    {
        private readonly RoleManager<IdentityRole> _roleManager;

        public RoleConfiguration(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }

        public async Task SeedAsync()
        {
            await _roleManager.CreateAsync(new IdentityRole(UserRole.Admin)
            {
                Id = "9409F310-E443-4F2E-8A1C-030A129C3FB2"
            });
            await _roleManager.CreateAsync(new IdentityRole(UserRole.User)
            {
                Id = "42D85232-B112-4635-B46C-D28F418CD92B"
            });
        }
    }
}
