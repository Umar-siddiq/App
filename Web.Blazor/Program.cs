using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Web.Blazor;


var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.Services.AddScoped<ProductApiClient>();
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");



builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:7015")
});

await builder.Build().RunAsync();
