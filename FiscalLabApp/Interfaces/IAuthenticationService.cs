using FiscalLabApp.Models;

namespace FiscalLabApp.Interfaces;

public interface IAuthenticationService
{
    Task<Result<LoginResult>> LoginAsync(LoginRequest loginRequest);
    Task LogoutAsync();
}