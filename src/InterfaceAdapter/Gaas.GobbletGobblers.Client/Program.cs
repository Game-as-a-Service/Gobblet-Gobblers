using Gaas.GobbletGobblers.Client;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

var environment = builder.HostEnvironment.Environment;
var baseAddress = builder.Configuration.GetValue<string>("BaseUrl");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(baseAddress!) });

await builder.Build().RunAsync();
