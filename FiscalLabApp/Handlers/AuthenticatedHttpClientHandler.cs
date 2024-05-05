using System.Net.Http.Headers;
using FiscalLabApp.Helpers;
using FiscalLabApp.Models;
using FiscalLabApp.Services;

namespace FiscalLabApp.Handlers;

public class AuthenticatedHttpClientHandler : DelegatingHandler
{
    private readonly IndexedDbAccessor _indexedDbAccessor;
    private const string TokenKey = "authToken";

    public AuthenticatedHttpClientHandler(IndexedDbAccessor indexedDbAccessor)
    {
        _indexedDbAccessor = indexedDbAccessor;
    }

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        var token = await _indexedDbAccessor.GetValueOrDefaultIdAsync<KeyValue>(CollectionsHelper.KeyValueCollection, TokenKey);
        if (token is not null)
        {
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token.Value);
        }
        return await base.SendAsync(request, cancellationToken);
    }
}