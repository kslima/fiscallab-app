using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using FiscalLabApp.Interfaces;
using FiscalLabApp.Models;

namespace FiscalLabApp.Services;

public class ApiService(IHttpClientFactory httpClientFactory) : IApiService
{
    private readonly HttpClient _httpClient = httpClientFactory.CreateClient("api");
    
    public async Task<VisitPage[]> GetAllVisitPagesAsync()
    {
        var result = await _httpClient.GetAsync("visit-pages");
        result.EnsureSuccessStatusCode();

        var response =  (await result.Content.ReadFromJsonAsync<ApiResponse<VisitPage[]>>())!;
        return response.Data!;
    }

    public async Task<Menu[]> GetAllMenusAsync()
    {
        var result = await _httpClient.GetAsync("menus");
        result.EnsureSuccessStatusCode();

        var response =  (await result.Content.ReadFromJsonAsync<ApiResponse<Menu[]>>())!;
        return response.Data!;
    }

    public async Task<Plant[]> GetAllPlantsAsync()
    {
        var result = await _httpClient.GetAsync("plants");
        result.EnsureSuccessStatusCode();

        var response =  (await result.Content.ReadFromJsonAsync<ApiResponse<Plant[]>>())!;
        return response.Data!;
    }

    public async Task<Association[]> GetAllAssociationsAsync()
    {
        var result = await _httpClient.GetAsync("associations");
        result.EnsureSuccessStatusCode();

        var response =  (await result.Content.ReadFromJsonAsync<ApiResponse<Association[]>>())!;
        return response.Data!;
    }

    public async Task<bool> CreateManyVisits(Visit[] visits)
    {
        var body = JsonSerializer.Serialize(visits);
        var contentString = new StringContent(body, Encoding.UTF8, "application/json");
        var result = await _httpClient.PostAsync("visits", contentString);
        result.EnsureSuccessStatusCode();

        var response =  (await result.Content.ReadFromJsonAsync<ApiResponse<bool>>())!;
        return response.Data!;
    }

    public async Task<string> GenerateVisitPdf(string visitId)
    {
        var result = await _httpClient.GetAsync($"visits/{visitId}/pdf");
        result.EnsureSuccessStatusCode();

        var response =  await result.Content.ReadAsByteArrayAsync();
        return Convert.ToBase64String(response);
    }

    public async Task<SyncResult> SyncDataAsync(SyncModel syncModel)
    {
        var result = await _httpClient.PostAsJsonAsync("synchronization", syncModel);
        result.EnsureSuccessStatusCode();

        var response =  (await result.Content.ReadFromJsonAsync<ApiResponse<SyncResult>>())!;
        return response.Data!;
    }
}