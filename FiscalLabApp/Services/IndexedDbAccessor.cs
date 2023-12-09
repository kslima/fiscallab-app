using System.Net.Http.Json;
using FiscalLabApp.Models;
using Microsoft.JSInterop;

namespace FiscalLabApp.Services;

public class IndexedDbAccessor(IJSRuntime jsRuntime,
    HttpClient httpClient) : IAsyncDisposable, IDisposable
{
    public const string OptionsCollectionName = "menus";
    private Lazy<IJSObjectReference> _accessorJsRef = new();

    public async Task InitializeAsync()
    {
        await WaitForReference();
        await _accessorJsRef.Value.InvokeVoidAsync("initialize");

        await InitializeOptionsAsync();
    }
    
    public async Task InitializeOptionsAsync()
    {
        var menus = await GetValueAsync<Menu[]>(OptionsCollectionName);
        if (menus.Length > 0) return;
        
        var result = await httpClient.GetFromJsonAsync<Menu[]>("/sample-data/menus.json");
        if (result != null)
        {
            foreach (var menu in result)
            {
                await SetValueAsync(OptionsCollectionName, menu);
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

    public void Dispose()
    {
        // if (_accessorJsRef.IsValueCreated)
        // {
        //     _accessorJsRef.Value.
        // }
    }
}