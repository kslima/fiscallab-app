using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using FiscalLabApp.Components;
using FiscalLabApp.Extensions;
using FiscalLabApp.Providers;
using FiscalLabApp.Services;
using Microsoft.AspNetCore.Components.Authorization;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddScoped<AuthenticationStateProvider, ApiAuthenticationStateProvider>();
builder.Services.AddBlazoredLocalStorage();

builder.Services.AddOptions();
builder.Services.AddAuthorizationCore(_ => { });

builder.Services.AddDependencies(builder.Configuration);

var host = builder.Build();

using var scope = host.Services.CreateScope();
await using var indexedDb = scope.ServiceProvider.GetService<IndexedDbAccessor>();

if (indexedDb is not null)
{
    await indexedDb.InitializeAsync();
}

await host.RunAsync();