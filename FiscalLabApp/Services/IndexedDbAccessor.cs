using System.Net.Http.Json;
using FiscalLabApp.Models;
using FiscalLabApp.Repositories.SqLite;
using Microsoft.EntityFrameworkCore;
using Microsoft.JSInterop;

namespace FiscalLabApp.Services;

public class IndexedDbAccessor : IAsyncDisposable, IDisposable
{
    private Lazy<IJSObjectReference> _accessorJsRef = new();
    private readonly IJSRuntime _jsRuntime;
    private readonly HttpClient _httpClient;
    private readonly IDbContextFactory<ApplicationDbContext> _dbContextFactory;

    public IndexedDbAccessor(
        IJSRuntime jsRuntime,
        HttpClient httpClient,
        IDbContextFactory<ApplicationDbContext> dbContextFactory)
    {
        _jsRuntime = jsRuntime;
        _httpClient = httpClient;
        _dbContextFactory = dbContextFactory;
    }

    public async Task InitializeAsync()
    {
        await WaitForReference();
        await _accessorJsRef.Value.InvokeVoidAsync("initialize");

        var result = await _httpClient.GetFromJsonAsync<MenuOption[]>("/sample-data/options.json");
        if (result != null)
        {
            foreach (var menu in result)
            {
                await SetValueAsync("options", menu);
            }
        }
    }

    private async Task WaitForReference()
    {
        if (!_accessorJsRef.IsValueCreated)
        {
            _accessorJsRef =
                new Lazy<IJSObjectReference>(
                    await _jsRuntime.InvokeAsync<IJSObjectReference>("import", "/js/IndexedDbAccessor.js"));
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

    public async Task<T> GetValueByKeyAsync<T>(string collectionName, string key)
    {
        await WaitForReference();
        var result = await _accessorJsRef.Value.InvokeAsync<T>("getByKey", collectionName, key);

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