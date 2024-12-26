namespace SchollApi;

public interface IAuthenticate
{
    Task<bool> Autenticate(string email, string password);
    Task<bool> Register(string email, string password);
    Task Logout();
}
