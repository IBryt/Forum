namespace Identity.API.Services;

public class EFLoginService : ILoginService<ApplicationUser>
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;

    public EFLoginService(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
    }

    public async Task<ApplicationUser> FindByUsername(string user)
    {
        var applicationUser = await _userManager.FindByEmailAsync(user);

        if (applicationUser == null)
        {
            throw new ArgumentException(nameof(applicationUser));
        }

        return applicationUser;
    }

    public async Task<bool> ValidateCredentials(ApplicationUser user, string password)
    {
        return await _userManager.CheckPasswordAsync(user, password);
    }

    public Task SignIn(ApplicationUser user)
    {
        return _signInManager.SignInAsync(user, true);
    }

    public Task SignInAsync(ApplicationUser user, AuthenticationProperties properties, string authenticationMethod)
    {
        return _signInManager.SignInAsync(user, properties, authenticationMethod);
    }
}
