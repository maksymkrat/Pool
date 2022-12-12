using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Pool.Client.Authentication;
using Pool.Client.Components;
using Pool.Client.Services;
using Pool.Shared.Models;

namespace Pool.Client.Pages;

public partial class Words_test : ComponentBase
{
    [Inject] private WordService _wordService { get; set; }
    [Inject] private AuthenticationStateProvider  _authStateProvider { get; set; }
    [Inject] private SessionService _sessionService { get; set; }
    protected WordModel MainWord { get; set; }
    protected List<WordModel> Words{ get; set; }
    protected NotificationType NotificationType { get; set; }
    protected string NotificationText { get; set; }
    protected Notification Notification { get; set; }
    protected bool DisplayNotification { get; set; }
    protected Random rnd = new Random();
    protected string resultStyle = "light";
    protected string mainWordStyle = "light";
    protected int rndWord = 0;
    


    protected  override void OnInitialized()
    {
        var user = _sessionService.UserSession;
        Words =  _wordService.GetFourRandomWords(user);
        if(Words.Count >= 4)
            MainWord = Words.ElementAt(rnd.Next(0, Words.Count)); //TODO: Notification You need to add at least 4 words
        InvokeAsync(StateHasChanged);
    }

    
    

    protected void ChangeStyleMainWord(string style)
    {
        mainWordStyle = style;
        
    }

    protected  void ResetWord()
    {
        OnInitialized();
        resultStyle = "light";
        mainWordStyle = resultStyle;
    }
    protected async void ShowHint()
    {
        NotificationType = NotificationType.Info;
        NotificationText = MainWord.Translation;
        DisplayNotification = true;
        InvokeAsync(StateHasChanged);
        await Task.Delay(3000);
        DisplayNotification = false;
        InvokeAsync(StateHasChanged);
    }
}