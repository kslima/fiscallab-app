using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using FiscalLabApp.Interfaces;
using FiscalLabApp.Models;

namespace FiscalLabApp.Services;

public class ApiService(IHttpClientFactory httpClientFactory) : IApiService
{
    private readonly HttpClient _httpClient = httpClientFactory.CreateClient("api");
    private readonly JsonSerializerOptions _jsonOptions = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower
    };

    public async Task<VisitPage[]> GetAllVisitPagesAsync()
    {
        var result = await _httpClient.GetAsync("visit-pages");
        result.EnsureSuccessStatusCode();

        var response =  (await result.Content.ReadFromJsonAsync<ApiResponse<VisitPage[]>>(_jsonOptions))!;
        return response.Data!;
    }

    public async Task<Menu[]> GetAllMenusAsync()
    {
        var result = await _httpClient.GetAsync("menus");
        result.EnsureSuccessStatusCode();

        var response =  (await result.Content.ReadFromJsonAsync<ApiResponse<Menu[]>>(_jsonOptions))!;
        return response.Data!;
    }

    public async Task<Plant[]> GetAllPlantsAsync()
    {
        var result = await _httpClient.GetAsync("plants");
        result.EnsureSuccessStatusCode();

        var response =  (await result.Content.ReadFromJsonAsync<ApiResponse<Plant[]>>(_jsonOptions))!;
        return response.Data!;
    }

    public async Task<Association[]> GetAllAssociationsAsync()
    {
        var result = await _httpClient.GetAsync("associations");
        result.EnsureSuccessStatusCode();

        var response =  (await result.Content.ReadFromJsonAsync<ApiResponse<Association[]>>(_jsonOptions))!;
        return response.Data!;
    }

    public async Task<bool> CreateManyVisits(Visit[] visits)
    {
        var body = JsonSerializer.Serialize(visits, _jsonOptions);
        var contentString = new StringContent(body, Encoding.UTF8, "application/json");
        var result = await _httpClient.PostAsync("visits", contentString);
        result.EnsureSuccessStatusCode();

        var response =  (await result.Content.ReadFromJsonAsync<ApiResponse<bool>>(_jsonOptions))!;
        return response.Data!;
    }
}