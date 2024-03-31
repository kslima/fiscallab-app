namespace FiscalLabApp.Models;

public class LoginResult
{
    public bool Success { get; set; }
    public string Token { get; set; } = string.Empty;
    public DateTime? ExpireAt { get; set; }
}