using Microsoft.AspNetCore.Identity;

namespace Weather.Api.Models
{
    public class WeatherContextConfiguration
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public WeatherContextConfiguration(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task SeedDataAsync()
        {
            var roleConfiguration = new RoleConfiguration(_roleManager);
            await roleConfiguration.SeedAsync();

            var userConfiguration = new UserConfiguration(_userManager);
            await userConfiguration.SeedAsync();
        }
    }
}
