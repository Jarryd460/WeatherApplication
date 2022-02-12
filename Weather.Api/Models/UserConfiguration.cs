using Microsoft.AspNetCore.Identity;

namespace Weather.Api.Models
{
    public class UserConfiguration
    {
        private readonly UserManager<IdentityUser> _userManager;

        public UserConfiguration(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task SeedAsync()
        {
            IdentityUser jonDoe = new IdentityUser("JonDoe")
            {
                Id = "BC048FA1-696C-45BC-95E2-6D72D94E4BFD",
                Email = "jon.doe@gmail.com",
                EmailConfirmed = true,
                PhoneNumber = "0824584753",
                PhoneNumberConfirmed = true,
            };

            await _userManager.CreateAsync(jonDoe, "JonDoe123$");
            await _userManager.AddToRolesAsync(jonDoe, new List<string>() { UserRole.Admin, UserRole.User });

            IdentityUser janeDoe = new IdentityUser("JaneDoe")
            {
                Id = "0BE6460A-8451-458B-A93F-874E4E7C970F",
                Email = "jane.doe@gmail.com",
                EmailConfirmed = true,
                PhoneNumber = "0731569534",
                PhoneNumberConfirmed = true
            };

            await _userManager.CreateAsync(janeDoe, "JaneDoe123$");
            await _userManager.AddToRoleAsync(janeDoe, UserRole.User);
        }
    }
}
