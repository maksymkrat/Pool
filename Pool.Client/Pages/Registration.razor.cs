using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Pool.Client.Components;
using Pool.Client.Services;
using Pool.Shared.Models;

namespace Pool.Client.Pages;

public class Registration_razor : ComponentBase
{
    [Inject] private AccountService _accountService { get; set; }
    [Inject] private NavigationManager _navManager { get; set; }
    [Inject] private IJSRuntime _js { get; set; }


    protected RegistrationModel newUser = new RegistrationModel();
    protected NotificationType NotificationType { get; set; }
    protected string NotificationText { get; set; }
    protected Notification Notification { get; set; }

    public void test()
    {
        HandleValidationSubmit();
    }

    protected async void HandleValidationSubmit()
    {
        //var result = await _accountService.Create(newUser);
        if (true)
        {
            NotificationType = NotificationType.Success;
            NotificationText = "Success";
            Notification.Show();
            await Task.Delay(2000);
            Notification.Hide();
            _navManager.NavigateTo("/Login", true);
        }
    }
}