using System.Net.Http.Json;
using System.Text.Json;
using FiscalLabApp.Interfaces;
using FiscalLabApp.Models;

namespace FiscalLabApp.Services;

public class ApiService(IHttpClientFactory httpClientFactory) : IApiService
{
    private readonly HttpClient _httpClient = httpClientFactory.CreateClient("api");

    public async Task<List<VisitPage>> GetAllVisitPagesAsync()
    {
        var options = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower
        };

        var result = await _httpClient.GetAsync("visit-pages");
        result.EnsureSuccessStatusCode();

        var response =  (await result.Content.ReadFromJsonAsync<ApiResponse<List<VisitPage>>>(options))!;
        return response.Data!;
    }
}