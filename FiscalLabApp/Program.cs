using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using FiscalLabApp.Components;
using FiscalLabApp.Extensions;
using FiscalLabApp.Resources;
using FiscalLabApp.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");


builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

var apiOptions = builder.Configuration
    .GetRequiredSection(nameof(ApiOptions))
    .Get<ApiOptions>()!;

builder.Services.AddSingleton(apiOptions);

builder.Services.AddHttpClient();
builder.Services.AddHttpClient("api", client => { client.BaseAddress = new Uri(apiOptions.BaseUrl); });


builder.Services.AddDependencies();

var host = builder.Build();

using var scope = host.Services.CreateScope();
await using var indexedDb = scope.ServiceProvider.GetService<IndexedDbAccessor>();

if (indexedDb is not null)
{
    await indexedDb.InitializeAsync();
}

await host.RunAsync();