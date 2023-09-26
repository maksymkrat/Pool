using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Pool.Client.Components;
using Pool.Client.Services;
using Pool.Shared.Models;

namespace Pool.Client.Pages;

public  class IrregularVerbTest_razor : ComponentBase
{
    
    [Inject] private IrregularVerbService _irregularVerbService { get; set; }
    [Inject] private SessionService _sessionService { get; set; }
    [Inject] private AuthenticationStateProvider  _authStateProvider { get; set; }

    public IrregularVerbModel Verb { get; set; }

    public string Infinitive { get; set; } = string.Empty;
    public string PastSimple { get; set; }= string.Empty;
    public string PastParticiple { get; set; }= string.Empty;
    public string TextStyleInf { get; set; }
    public string TextStylePS { get; set; }
    public string TextStylePP { get; set; }
    
    protected NotificationType NotificationType { get; set; }
    protected string NotificationText { get; set; }
    protected Notification Notification { get; set; }
    protected bool DisplayNotification { get; set; }

    protected override void OnInitialized()
    {
        var user = _sessionService.UserSession;
        Verb = _irregularVerbService.GetRandomIrregularVerb(user);
        base.OnInitialized();
    }
    
    
    protected void ResetWord()
    {
        var user = _sessionService.UserSession;
        Verb = _irregularVerbService.GetRandomIrregularVerb(user);
        Infinitive = string.Empty;
        PastSimple = string.Empty;
        PastParticiple = string.Empty;
        TextStyleInf = "dark"; // nead refactor
        TextStylePS = "dark";
        TextStylePP = "dark";
    }
    protected void CheckVerb()
    {
        _ = Verb.Infinitive.Trim() == Infinitive.Trim() ? TextStyleInf = "success" : TextStyleInf = "danger";
        _ = Verb.PastSimple.Trim() == PastSimple.Trim() ? TextStylePS = "success" : TextStylePS = "danger";
        _ = Verb.PastParticiple.Trim() == PastParticiple.Trim() ? TextStylePP = "success" : TextStylePP = "danger";
    }

    protected async void ShowHint()
    {
        NotificationType = NotificationType.Info;
        NotificationText = $"{Verb.Infinitive}, {Verb.PastSimple}, {Verb.PastParticiple}";
        DisplayNotification = true;
        InvokeAsync(StateHasChanged);
        await Task.Delay(3000);
        DisplayNotification = false;
        InvokeAsync(StateHasChanged);
    }
}