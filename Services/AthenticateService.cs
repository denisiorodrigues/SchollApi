
using Microsoft.AspNetCore.Identity;

namespace SchollApi;

public class AthenticateService : IAuthenticate
{
    private readonly SignInManager<IdentityUser> _signInManager;

    public AthenticateService(SignInManager<IdentityUser> signInManager)
    {
        _signInManager = signInManager;
    }

    public async Task<bool> Autenticate(string email, string password)
    {
        var result = await _signInManager.PasswordSignInAsync(email, password, false, lockoutOnFailure: false);

        return result.Succeeded;
    }

    public async Task Logout()
    {
        await _signInManager.SignOutAsync();
    }
}
