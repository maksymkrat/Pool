using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Pool.UI;
using Pool.UI.Authentication;
using Pool.UI.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

//builder.Services.AddScoped(sp => new HttpClient {BaseAddress = new Uri("http://localhost:1111")});

builder.Services.AddBlazoredLocalStorage();
builder.Services.AddScoped<AccountService>();
builder.Services.AddScoped<WordService>();
builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthenticationStateProvider>();
builder.Services.AddAuthorizationCore();
await builder.Build().RunAsync();

