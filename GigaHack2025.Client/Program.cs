using GigaHack2025.Client;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

// Update HttpClient to point to your actual server URL
builder.Services.AddScoped(sp => new HttpClient
{
    BaseAddress = new Uri("https://localhost:64756/") // Your actual server HTTPS port
});


await builder.Build().RunAsync();

var app = builder.Build();
await builder.Build().RunAsync();
