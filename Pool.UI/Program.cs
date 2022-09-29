using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Pool.UI;
using Pool.UI.Authentication;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient {BaseAddress = new Uri("http://localhost:1111")});

builder.Services.AddBlazoredLocalStorage();

builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthenticationStateProvider>();
builder.Services.AddAuthorizationCore();
await builder.Build().RunAsync();

//      example http query
// services.AddHttpClient("myapi", c =>
// {
//     c.BaseAddress = new Uri("http://localhost:57025/api/");
// }); 

// var request = new HttpRequestMessage(HttpMethod.Get,"Mypage");
// var client = _clientFactory.CreateClient("myapi");
// var response = await client.SendAsync(request);

//------------------------
// HttpClient client = new HttpClient();
// client.BaseAddress = new Uri("http://localhost:57025/api/");
// var response = await client.GetAsync("Mypage");