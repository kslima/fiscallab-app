using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using FiscalLabApp.Helpers;
using FiscalLabApp.Interfaces;
using FiscalLabApp.Models;
using FiscalLabApp.Providers;
using Microsoft.AspNetCore.Components.Authorization;

namespace FiscalLabApp.Services;

public class ApiService(
    IHttpClientFactory httpClientFactory,
    AuthenticationStateProvider authenticationStateProvider,
    IndexedDbAccessor indexedDbAccessor) : IApiService, IAuthenticationService
{
    private readonly HttpClient _httpClient = httpClientFactory.CreateClient("api");

    public async Task<ApiResponse<Plant>> CreatePlantAsync(Plant plant)
    {
        var result = await _httpClient.PostAsJsonAsync("plants", plant);
        result.EnsureSuccessStatusCode();

        return (await result.Content.ReadFromJsonAsync<ApiResponse<Plant>>())!;
    }
    
    public async Task<ApiResponse<Plant>> UpdatePlantAsync(string plantId, Plant plant)
    {
        var result = await _httpClient.PutAsJsonAsync($"plants/{plantId}", plant);
        result.EnsureSuccessStatusCode();

        return (await result.Content.ReadFromJsonAsync<ApiResponse<Plant>>())!;
    }

    public async Task<VisitPage[]> GetAllVisitPagesAsync()
    {
        var result = await _httpClient.GetAsync("visit-pages");
        result.EnsureSuccessStatusCode();

        var response = (await result.Content.ReadFromJsonAsync<ApiResponse<VisitPage[]>>())!;
        return response.Data!;
    }

    public async Task<Menu[]> GetAllMenusAsync()
    {
        var result = await _httpClient.GetAsync("menus");
        result.EnsureSuccessStatusCode();

        var response = (await result.Content.ReadFromJsonAsync<ApiResponse<Menu[]>>())!;
        return response.Data!;
    }

    public async Task<Plant[]> GetAllPlantsAsync()
    {
        var result = await _httpClient.GetAsync("plants");
        result.EnsureSuccessStatusCode();

        var response = (await result.Content.ReadFromJsonAsync<ApiResponse<Plant[]>>())!;
        return response.Data!;
    }

    public async Task<Association[]> GetAllAssociationsAsync()
    {
        var result = await _httpClient.GetAsync("associations");
        result.EnsureSuccessStatusCode();

        var response = (await result.Content.ReadFromJsonAsync<ApiResponse<Association[]>>())!;
        return response.Data!;
    }

    public async Task<bool> CreateManyVisits(Visit[] visits)
    {
        var body = JsonSerializer.Serialize(visits);
        var contentString = new StringContent(body, Encoding.UTF8, "application/json");
        var result = await _httpClient.PostAsync("visits", contentString);
        result.EnsureSuccessStatusCode();

        var response = (await result.Content.ReadFromJsonAsync<ApiResponse<bool>>())!;
        return response.Data!;
    }

    public async Task<byte[]> GenerateVisitPdf(string visitId)
    {
        var result = await _httpClient.GetAsync($"visits/{visitId}/pdf");
        result.EnsureSuccessStatusCode();

        return await result.Content.ReadAsByteArrayAsync();
    }

    public async Task<SyncResult> SyncDataAsync(SyncModel syncModel)
    {
        var result = await _httpClient.PostAsJsonAsync("synchronization", syncModel);
        result.EnsureSuccessStatusCode();

        var response = (await result.Content.ReadFromJsonAsync<ApiResponse<SyncResult>>())!;
        return response.Data!;
    }

    public async Task<Result<LoginResult>> LoginAsync(LoginRequest loginRequest)
    {
        var loginResult = await _httpClient.PostAsJsonAsync("accounts/login", loginRequest);
        if (loginResult.IsSuccessStatusCode)
        {
            var response = (await loginResult.Content.ReadFromJsonAsync<ApiResponse<LoginResult>>())!;
            var token = response.Data!.Token;

            await indexedDbAccessor.SetValueAsync(CollectionsHelper.KeyValueCollection,
                new KeyValue { Id = ApiAuthenticationStateProvider.TokenKey, Value = token });
            ((ApiAuthenticationStateProvider)authenticationStateProvider).MarkUserAuthenticated(loginRequest.Email);

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            return Result<LoginResult>.Success(response.Data!);
        }

        if (loginResult.StatusCode == HttpStatusCode.Unauthorized)
        {
            return Result<LoginResult>.Failure(new Error(nameof(HttpStatusCode.Unauthorized), "Usuário ou senha incorretos."));
        }

        return Result<LoginResult>.Failure(new Error(nameof(HttpStatusCode.InternalServerError),
            "Aconteceu um erro. Por favor, tente novamente mais tarde.."));
    }

    public async Task LogoutAsync()
    {
        await indexedDbAccessor.DeleteAsync(CollectionsHelper.KeyValueCollection, ApiAuthenticationStateProvider.TokenKey);
        ((ApiAuthenticationStateProvider)authenticationStateProvider).MarkUserLoggedOut();
        _httpClient.DefaultRequestHeaders.Authorization = null;
    }
}