using FiscalLabApp.Interfaces;
using Microsoft.JSInterop;

namespace FiscalLabApp.Services;

public class IndexedDbAccessor(
    IJSRuntime jsRuntime,
    IApiService apiService) : IDisposable, IAsyncDisposable
{
    public const string MenuCollectionName = "menus";
    private const string PlantCollectionName = "plants";
    private const string AssociationCollectionName = "associations";

    private Lazy<IJSObjectReference> _accessorJsRef = new();

    public async Task InitializeAsync()
    {
        await WaitForReference();
        await _accessorJsRef.Value.InvokeVoidAsync("initialize");
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
    
    public async ValueTask DisposeAsync()
    {
        if (_accessorJsRef.IsValueCreated)
        {
            await _accessorJsRef.Value.DisposeAsync();
        }
    }

    public void Dispose()
    {
        if (_accessorJsRef.IsValueCreated)
        {
            _accessorJsRef.Value.DisposeAsync();
        }
    }
}