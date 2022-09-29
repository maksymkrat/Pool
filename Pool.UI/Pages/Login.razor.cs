using System.Net;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.JSInterop;
using Pool.Shared.Models;
using Pool.UI.Authentication;

namespace Pool.UI.Pages;

public  class Login_razor : ComponentBase
{
    [Inject]  HttpClient httpClient { get; set; }
    [Inject] private IJSRuntime js { get; set; }
    [Inject] private AuthenticationStateProvider authStateProvider { get; set; }
    [Inject] private NavigationManager navManager { get; set; }
    
    protected LoginRequest loginRequest = new LoginRequest();

    protected async Task Authentication()
    {
        try
        {
            var LoginResponse = await httpClient.PostAsJsonAsync<LoginRequest>("/api/account/login", loginRequest);
            if (LoginResponse.IsSuccessStatusCode)
            {
                var userSession = await LoginResponse.Content.ReadFromJsonAsync<UserSession>();
                var customAuthStateProvider = (CustomAuthenticationStateProvider)authStateProvider;
                await customAuthStateProvider.UpdateAuthenticationState(userSession);
                navManager.NavigateTo("/", true);
            }
            else if(LoginResponse.StatusCode == HttpStatusCode.Unauthorized)
            {
                await js.InvokeVoidAsync("alert", "Invalid User Name or Password");
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            
        }
       
    }
    
}