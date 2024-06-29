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

    public async Task<ApiResponse<Association>> CreateAssociationAsync(Association association)
    {
        var result = await _httpClient.PostAsJsonAsync("associations", association);
        result.EnsureSuccessStatusCode();

        return (await result.Content.ReadFromJsonAsync<ApiResponse<Association>>())!;
    }

    public async Task<ApiResponse<Association>> UpdateAssociationAsync(string associationId, Association association)
    {
        var result = await _httpClient.PutAsJsonAsync($"associations/{associationId}", association);
        result.EnsureSuccessStatusCode();

        return (await result.Content.ReadFromJsonAsync<ApiResponse<Association>>())!;
    }

    public async Task<VisitPage[]> GetAllVisitPagesAsync()
    {
        var result = await _httpClient.GetAsync("visit-pages");
        result.EnsureSuccessStatusCode();

        var response = (await result.Content.ReadFromJsonAsync<ApiResponse<VisitPage[]>>())!;
        return response.Data!;
    }

    public async Task<Menu[]> ListOptionsAsync()
    {
        var result = await _httpClient.GetAsync("menus");
        result.EnsureSuccessStatusCode();

        var response = (await result.Content.ReadFromJsonAsync<ApiResponse<Menu[]>>())!;
        return response.Data!;
    }

    public async Task<Plant[]> ListPlantsAsync()
    {
        var result = await _httpClient.GetAsync("plants");
        result.EnsureSuccessStatusCode();

        var response = (await result.Content.ReadFromJsonAsync<ApiResponse<Plant[]>>())!;
        return response.Data!;
    }

    public async Task<Association[]> ListAssociationsAsync()
    {
        var result = await _httpClient.GetAsync("associations");
        result.EnsureSuccessStatusCode();

        var response = (await result.Content.ReadFromJsonAsync<ApiResponse<Association[]>>())!;
        return response.Data!;
    }
    
    public async Task<ApiResponse<bool>> DeleteAssociationAsync(string associationId)
    {
        var result = await _httpClient.DeleteAsync($"associations/{associationId}");
        result.EnsureSuccessStatusCode();

        return (await result.Content.ReadFromJsonAsync<ApiResponse<bool>>())!;
    }
    
    public async Task<ApiResponse<bool>> DeletePlantAsync(string plantId)
    {
        var result = await _httpClient.DeleteAsync($"plants/{plantId}");
        result.EnsureSuccessStatusCode();

        return (await result.Content.ReadFromJsonAsync<ApiResponse<bool>>())!;
    }
    
    private static string ReplaceFirstAmpersand(string input)
    {
        var index = input.IndexOf('&');
        if (index != -1)
        {
            input = input.Remove(index, 1).Insert(index, "?");
        }
        return input;
    }
    
    public async Task<ApiResponse<Visit[]>> ListVisitsAsync(VisitParameters parameters)
    {
        var queryBuilder = new StringBuilder("visits");
        if (parameters.PageIndex is not null)
        {
            queryBuilder.Append($"&PageIndex={parameters.PageIndex}");
        }
        
        if (parameters.PageSize is not null)
        {
            queryBuilder.Append($"&PageSize={parameters.PageSize}");
        }
        
        if (parameters.Status is not null)
        {
            queryBuilder.Append($"&status={parameters.Status}");
        }
        
        var result = await _httpClient.GetAsync(ReplaceFirstAmpersand(queryBuilder.ToString()));
        result.EnsureSuccessStatusCode();

        return (await result.Content.ReadFromJsonAsync<ApiResponse<Visit[]>>())!;
    }

    public async Task<ApiResponse<bool>> DeleteVisitImageAsync(string visitId, string imageId)
    {
        var result = await _httpClient.DeleteAsync($"visits/{visitId}/images/{imageId}");
        result.EnsureSuccessStatusCode();

        return (await result.Content.ReadFromJsonAsync<ApiResponse<bool>>())!;
    }
    
    public async Task<ApiResponse<bool>> UpsertImagesAsync(string id, List<Image> images)
    {
        var content = new MultipartFormDataContent();
        foreach (var image in images)
        {
            var byteContent = new ByteArrayContent(image.Data);
            var contentType = GetContentType(image.Name);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue(contentType);
            
            content.Add(byteContent, $"file_{image.Id}", image.Name);
            content.Add(new StringContent(image.Name), $"name_{image.Id}");
            content.Add(new StringContent(image.Description), $"description_{image.Id}");
        }

        try
        {
            var result = await _httpClient.PostAsync($"visits/{id}/images", content);
            result.EnsureSuccessStatusCode();

            return (await result.Content.ReadFromJsonAsync<ApiResponse<bool>>())!;
        }
        catch (Exception)
        {
            return new ApiResponse<bool>
            {
                Data = false
            };
        }
    }

    public async Task<ApiResponse<Image[]>> ListImagesAsync(string id)
    {
        try
        {
            var result = await _httpClient.GetAsync($"visits/{id}/images");
            result.EnsureSuccessStatusCode();

            return (await result.Content.ReadFromJsonAsync<ApiResponse<Image[]>>())!;
        }
        catch (Exception)
        {
            return new ApiResponse<Image[]>
            {
                Data = []
            };
        }
    }

    private static string GetContentType(string fileName)
    {
        var extension = Path.GetExtension(fileName).ToLowerInvariant();
        return extension switch
        {
            ".jpg" or ".jpeg" => "image/jpeg",
            ".png" => "image/png",
            ".gif" => "image/gif",
            ".bmp" => "image/bmp",
            ".tiff" => "image/tiff",
            ".webp" => "image/webp",
            _ => "application/octet-stream", 
        };
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

    public async Task<Visit> GetVisitByIdAsync(string visitId)
    {
        var result = await _httpClient.GetAsync($"visits/{visitId}");
        result.EnsureSuccessStatusCode();
        
        var response = (await result.Content.ReadFromJsonAsync<ApiResponse<Visit>>())!;
        return response.Data!;
    }

    public async Task<bool> DeleteVisitAsync(string visitId)
    {
        var result = await _httpClient.DeleteAsync($"visits/{visitId}");
        result.EnsureSuccessStatusCode();

        var response = (await result.Content.ReadFromJsonAsync<ApiResponse<bool>>())!;
        return response.Data!;
    }

    public async Task<bool> UpsertVisitsAsync(Visit[] visits)
    {
        var result = await _httpClient.PostAsJsonAsync("visits/upsert", visits);
        result.EnsureSuccessStatusCode();

        var response = (await result.Content.ReadFromJsonAsync<ApiResponse<bool>>())!;
        return response.Data;
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