using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using FiscalLabApp.Components;
using FiscalLabApp.Extensions;
using FiscalLabApp.Providers;
using FiscalLabApp.Resources;
using FiscalLabApp.Services;
using Microsoft.AspNetCore.Components.Authorization;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");


builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddScoped<AuthenticationStateProvider, ApiAuthenticationStateProvider>();
builder.Services.AddBlazoredLocalStorage();

var apiOptions = builder.Configuration
    .GetRequiredSection(nameof(ApiOptions))
    .Get<ApiOptions>()!;

builder.Services.AddSingleton(apiOptions);

builder.Services.AddHttpClient();
builder.Services.AddHttpClient("api", client => { client.BaseAddress = new Uri(apiOptions.BaseUrl); });

builder.Services.AddOptions();
builder.Services.AddAuthorizationCore(_ => { });

builder.Services.AddDependencies();

var host = builder.Build();

using var scope = host.Services.CreateScope();
await using var indexedDb = scope.ServiceProvider.GetService<IndexedDbAccessor>();

if (indexedDb is not null)
{
    await indexedDb.InitializeAsync();
}

await host.RunAsync();