using FiscalLabApp.Interfaces;
using FiscalLabApp.Models;
using Microsoft.JSInterop;

namespace FiscalLabApp.Services;

public class IndexedDbAccessor(
    IJSRuntime jsRuntime,
    IApiService apiService) : IAsyncDisposable
{
    public const string MenuCollectionName = "menus";
    private const string PlantCollectionName = "plants";
    private const string AssociationCollectionName = "associations";
    public const string VisitPageCollectionName = "visit_pages";
    
    private Lazy<IJSObjectReference> _accessorJsRef = new();

    public async Task InitializeAsync()
    {
        await WaitForReference();
        await _accessorJsRef.Value.InvokeVoidAsync("initialize");

        await InitializeVisitPagesAsync();
        await InitializeMenusAsync();
        await InitializePlantsAsync();
        await InitializeAssociationsAsync();
    }

    private async Task InitializeVisitPagesAsync()
    {
        var visitPages =  await GetValueAsync<VisitPage[]>(VisitPageCollectionName);
        if (visitPages.Length != 0) return;
        
        visitPages = await apiService.GetAllVisitPagesAsync();
        if (visitPages.Length == 0) return;
        
        foreach (var visitPage in visitPages)
        {
            await SetValueAsync(VisitPageCollectionName, visitPage);
        }
    }
    
    private async Task InitializeMenusAsync()
    {
        var menus =  await GetValueAsync<Menu[]>(MenuCollectionName);
        if (menus.Length != 0) return;
        
        menus = await apiService.GetAllMenusAsync();
        if (menus.Length == 0) return;
        foreach (var menu in menus)
        {
            await SetValueAsync(MenuCollectionName, menu);
        }
    }
    
    private async Task InitializePlantsAsync()
    {
        var plants =  await GetValueAsync<Plant[]>(PlantCollectionName);
        if (plants.Length != 0) return;
        
        plants = await apiService.GetAllPlantsAsync();
        if (plants.Length == 0) return;
        foreach (var plant in plants)
        {
            await SetValueAsync(PlantCollectionName, plant);
        }
    }
    
    private async Task InitializeAssociationsAsync()
    {
        var associations =  await GetValueAsync<Association[]>(AssociationCollectionName);
        if (associations.Length != 0) return;
        
        associations = await apiService.GetAllAssociationsAsync();
        if (associations.Length == 0) return;
        foreach (var association in associations)
        {
            await SetValueAsync(AssociationCollectionName, association);
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