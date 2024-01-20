using System.Net.Http.Json;
using FiscalLabApp.Models;
using Microsoft.JSInterop;

namespace FiscalLabApp.Services;

public class IndexedDbAccessor(IJSRuntime jsRuntime,
    HttpClient httpClient) : IAsyncDisposable
{
    public const string MenuCollectionName = "menus";
    public const string PlantCollectionName = "plants";
    public const string AssociationCollectionName = "associations";
    
    private Lazy<IJSObjectReference> _accessorJsRef = new();

    public async Task InitializeAsync()
    {
        await WaitForReference();
        await _accessorJsRef.Value.InvokeVoidAsync("initialize");

        await InitializeOptionsAsync();
        await InitializePlantsAsync();
        await InitializeAssociationsAsync();
    }
    
    public async Task InitializeOptionsAsync()
    {
        var menus = await GetValueAsync<Menu[]>(MenuCollectionName);
        if (menus.Length > 0) return;
        
        var result = await httpClient.GetFromJsonAsync<Menu[]>("/sample-data/menus.json");
        if (result != null)
        {
            foreach (var menu in result)
            {
                await SetValueAsync(MenuCollectionName, menu);
            }
        }
    }
    
    public async Task InitializePlantsAsync()
    {
        var menus = await GetValueAsync<PlantModel[]>(PlantCollectionName);
        if (menus.Length > 0) return;
        
        var result = await httpClient.GetFromJsonAsync<PlantModel[]>("/sample-data/plants.json");
        if (result != null)
        {
            foreach (var menu in result)
            {
                await SetValueAsync(PlantCollectionName, menu);
            }
        }
    }
    
    public async Task InitializeAssociationsAsync()
    {
        var menus = await GetValueAsync<AssociationModel[]>(AssociationCollectionName);
        if (menus.Length > 0) return;
        
        var result = await httpClient.GetFromJsonAsync<AssociationModel[]>("/sample-data/associations.json");
        if (result != null)
        {
            foreach (var menu in result)
            {
                await SetValueAsync(AssociationCollectionName, menu);
            }
        }
    }

    private async Task WaitForReference()
    {
        if (!_accessorJsRef.IsValueCreated)
        {
            _accessorJsRef =
                new Lazy<IJSObjectReference>(
                    await jsRuntime.InvokeAsync<IJSObjectReference>("import", "/js/IndexedDbAccessor.js"));
        }
    }

    public async ValueTask DisposeAsync()
    {
        if (_accessorJsRef.IsValueCreated)
        {
            await _accessorJsRef.Value.DisposeAsync();
        }
    }

    public async Task<T> GetValueAsync<T>(string collectionName)
    {
        await WaitForReference();
        var result = await _accessorJsRef.Value.InvokeAsync<T>("get", collectionName);

        return result;
    }

    public async Task<T> GetValueByIdAsync<T>(string collectionName, string key)
    {
        await WaitForReference();
        var result = await _accessorJsRef.Value.InvokeAsync<T>("getById", collectionName, key);

        return result;
    }
    
    public async Task DeleteAsync(string collectionName, string key)
    {
        await WaitForReference();
        await _accessorJsRef.Value.InvokeVoidAsync("remove", collectionName, key);
    }

    public async Task SetValueAsync<T>(string collectionName, T value)
    {
        await WaitForReference();
        await _accessorJsRef.Value.InvokeVoidAsync("set", collectionName, value);
    }

    public async Task DeleteDatabaseAsync()
    {
        await WaitForReference();
        await _accessorJsRef.Value.InvokeVoidAsync("deleteDatabase");
    }
}