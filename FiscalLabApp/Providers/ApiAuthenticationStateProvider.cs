using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text.Json;
using FiscalLabApp.Helpers;
using FiscalLabApp.Models;
using FiscalLabApp.Services;
using Microsoft.AspNetCore.Components.Authorization;

namespace FiscalLabApp.Providers;

public class ApiAuthenticationStateProvider : AuthenticationStateProvider
{
    private readonly HttpClient _httpClient;
    private readonly IndexedDbAccessor _indexedDbAccessor;
    public const string AuthenticationType = "apiauth";
    public const string TokenKey = "authToken";

    public ApiAuthenticationStateProvider(
        IHttpClientFactory httpClientFactory, IndexedDbAccessor indexedDbAccessor)
    {
        _httpClient = httpClientFactory.CreateClient("api");
        _indexedDbAccessor = indexedDbAccessor;
    }

    public void MarkUserAuthenticated(string email)
    {
        var authenticatedUser =
            new ClaimsPrincipal(new ClaimsIdentity(
                new[] { new Claim(ClaimTypes.Email, email), new Claim(ClaimTypes.Name, email) }, AuthenticationType));
        var authState = Task.FromResult(new AuthenticationState(authenticatedUser));
        NotifyAuthenticationStateChanged(authState);
    }

    public void MarkUserLoggedOut()
    {
        var anonymousUser =
            new ClaimsPrincipal(new ClaimsIdentity());
        var authState = Task.FromResult(new AuthenticationState(anonymousUser));
        NotifyAuthenticationStateChanged(authState);
    }

    private IEnumerable<Claim> ParseClaimsFromJwt(string jwt)
    {
        var claims = new List<Claim>();
        var payload = jwt.Split('.')[1];
        var jsonBytes = ParseBase64WithoutPadding(payload);
        var keyValuePairs = JsonSerializer.Deserialize<Dictionary<string, object>>(jsonBytes)!;

        if (keyValuePairs!.TryGetValue(ClaimTypes.Role, out object? roles))
        {
            if (roles.ToString()!.Trim().StartsWith("["))
            {
                var parsedRoles = JsonSerializer.Deserialize<string[]>(roles.ToString()!)!;
                foreach (var parsedRole in parsedRoles)
                {
                    claims.Add(new Claim(ClaimTypes.Role, parsedRole));
                }
            }
            else
            {
                claims.Add(new Claim(ClaimTypes.Role, roles.ToString()!));
            }

            keyValuePairs.Remove(ClaimTypes.Role);
        }

        if (keyValuePairs.TryGetValue(ClaimTypes.Name, out object? name))
        {
            claims.Add(new Claim(ClaimTypes.Name, name.ToString()!));
            keyValuePairs.Remove(ClaimTypes.Name);
        }

        claims.AddRange(keyValuePairs.Select(x => new Claim(x.Key, x.Value.ToString()!)));
        return claims;
    }

    private byte[] ParseBase64WithoutPadding(string base64)
    {
        switch (base64.Length % 4)
        {
            case 2:
                base64 += "==";
                break;
            case 3:
                base64 += "=";
                break;
        }

        return Convert.FromBase64String(base64);
    }

    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        var savedToken =
            await _indexedDbAccessor.GetValueOrDefaultIdAsync<KeyValue>(CollectionsHelper.KeyValueCollection, TokenKey);
        if (savedToken is null)
        {
            return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
        }
        
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", savedToken.Value);
        var claims = ParseClaimsFromJwt(savedToken.Value).ToList();
        return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity(claims, "jwt")));
    }
}