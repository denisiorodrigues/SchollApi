
using Microsoft.AspNetCore.Identity;

namespace SchollApi;

public class AuthenticateService : IAuthenticate
{
    private readonly SignInManager<IdentityUser> _signInManager;
    private readonly UserManager<IdentityUser> _userManager;

    public AuthenticateService(SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager)
    {
        _signInManager = signInManager;
        _userManager = userManager;
    }

    public async Task<bool> Autenticate(string email, string password)
    {
        var result = await _signInManager.PasswordSignInAsync(email, password, false, lockoutOnFailure: false);

        return result.Succeeded;
    }

    public async Task<bool> Register(string email, string password)
    {
        var identityUser = new IdentityUser() 
        {
            UserName = email,
            Email = email
        };

        var result = await _userManager.CreateAsync(identityUser, password);

        if(result.Succeeded)
        {
            await _signInManager.SignInAsync(identityUser, isPersistent: false);
        }

        return result.Succeeded;
    }

    public async Task Logout()
    {
        await _signInManager.SignOutAsync();
    }
}
