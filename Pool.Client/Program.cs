using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using Pool.Client.Authentication;
using Pool.Client.Services;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddBlazoredLocalStorage();
builder.Services.AddAuthenticationCore();

builder.Services.AddScoped<AccountService>();
builder.Services.AddSingleton<SessionService>();
builder.Services.AddScoped<WordService>();
builder.Services.AddScoped<IrregularVerbService>();
 //builder.Services.AddScoped<SpeechService>(); 
builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthenticationStateProvider>();

builder.Host.UseWindowsService();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();