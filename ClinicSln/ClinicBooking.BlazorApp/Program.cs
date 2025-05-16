using ClinicBooking.BlazorApp;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient
{
    BaseAddress = new Uri("http://localhost:5136/api")
});
builder.Services.AddAuthorizationCore(options =>
{
    options.AddPolicy("Authenticated", policy => policy.RequireAuthenticatedUser());
});
await builder.Build().RunAsync();
