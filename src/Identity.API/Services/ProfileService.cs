﻿namespace Identity.API.Services;

public class ProfileService : IProfileService
{
    private readonly UserManager<ApplicationUser> _userManager;

    public ProfileService(UserManager<ApplicationUser> userManager)
    {
        _userManager = userManager;
    }

    public async Task GetProfileDataAsync(ProfileDataRequestContext context)
    {
        var subject = context.Subject ?? throw new ArgumentNullException(nameof(context.Subject));

        var subjectId = GetSubjectId(subject);

        var user = await _userManager.FindByIdAsync(subjectId);

        if (user == null)
        {
            throw new ArgumentException("Invalid subject identifier");
        }

        var claims = GetClaimsFromUser(user);

        context.IssuedClaims = claims.ToList();
    }

    public async Task IsActiveAsync(IsActiveContext context)
    {
        var subject = context.Subject ?? throw new ArgumentNullException(nameof(context.Subject));

        var subjectId = GetSubjectId(subject);

        var user = await _userManager.FindByIdAsync(subjectId);

        context.IsActive = false;

        if (user != null)
        {
            if (_userManager.SupportsUserSecurityStamp)
            {
                var security_stamp = subject.Claims.Where(c => c.Type == "security_stamp").Select(c => c.Value).SingleOrDefault();
                if (security_stamp != null)
                {
                    var db_security_stamp = await _userManager.GetSecurityStampAsync(user);
                    if (db_security_stamp != security_stamp)
                        return;
                }
            }

            context.IsActive =
                !user.LockoutEnabled ||
                !user.LockoutEnd.HasValue ||
                user.LockoutEnd <= DateTime.UtcNow;
        }
    }

    private static string GetSubjectId(ClaimsPrincipal subject)
    {
        return subject.Claims.Where(x => x.Type == "sub").FirstOrDefault()?.Value
            ?? throw new Exception("SubjectId is missing in the claims.");
    }

    private IEnumerable<Claim> GetClaimsFromUser(ApplicationUser user)
    {
        if (user?.UserName == null)
        {
            throw new ArgumentNullException(nameof(user.UserName));
        }

        if (user?.Email == null)
        {
            throw new ArgumentNullException(nameof(user.Email));
        }

        var claims = new List<Claim>
        {
            new Claim(JwtClaimTypes.Subject, user.Id),
            new Claim(JwtClaimTypes.PreferredUserName, user.UserName),
            new Claim(JwtRegisteredClaimNames.UniqueName, user.UserName)
        };

        if (_userManager.SupportsUserEmail)
        {
            claims.AddRange(new[]
            {
                new Claim(JwtClaimTypes.Email, user.Email),
                new Claim(JwtClaimTypes.EmailVerified, user.EmailConfirmed ? "true" : "false", ClaimValueTypes.Boolean)
            });
        }

        if (_userManager.SupportsUserPhoneNumber && !string.IsNullOrWhiteSpace(user.PhoneNumber))
        {
            claims.AddRange(new[]
            {
                new Claim(JwtClaimTypes.PhoneNumber, user.PhoneNumber),
                new Claim(JwtClaimTypes.PhoneNumberVerified, user.PhoneNumberConfirmed ? "true" : "false", ClaimValueTypes.Boolean)
            });
        }

        return claims;
    }
}
