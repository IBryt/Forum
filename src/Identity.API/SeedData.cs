namespace Identity.API;

public class UsersSeed(ILogger<UsersSeed> _logger, UserManager<ApplicationUser> _userManager) : IDbSeeder<ApplicationDbContext>
{
    public async Task SeedAsync(ApplicationDbContext context)
    {
        var alice = await _userManager.FindByNameAsync("alice");

        if (alice == null)
        {
            alice = new ApplicationUser
            {
                UserName = "alice",
                Email = "AliceSmith@email.com",
                EmailConfirmed = true,
            };

            var result = await _userManager.CreateAsync(alice, "Pass123$");

            if (!result.Succeeded)
            {
                throw new Exception(result.Errors.First().Description);
            }

            if (_logger.IsEnabled(LogLevel.Debug))
            {
                _logger.LogDebug("alice created");
            }
        }
        else
        {
            _logger.LogDebug("alice already exists");
        }

        var bob = await _userManager.FindByNameAsync("bob");

        if (bob == null)
        {
            bob = new ApplicationUser
            {
                UserName = "bob",
                Email = "BobSmith@email.com",
                EmailConfirmed = true
            };

            var result = await _userManager.CreateAsync(bob, "Pass123$");

            if (!result.Succeeded)
            {
                throw new Exception(result.Errors.First().Description);
            }

            if (_logger.IsEnabled(LogLevel.Debug))
            {
                _logger.LogDebug("bob created");
            }
        }
        else
        {
            _logger.LogDebug("bob already exists");
        }
    }
}
