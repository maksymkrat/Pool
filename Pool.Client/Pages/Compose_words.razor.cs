using System.Text;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Pool.Client.Authentication;
using Pool.Client.Components;
using Pool.Client.Services;
using Pool.Shared.Models;

namespace Pool.Client.Pages;

public class Compose_words_razor : ComponentBase
{
    [Inject] private WordService _wordService { get; set; }
    [Inject] private SessionService _sessionService { get; set; }
    [Inject] private AuthenticationStateProvider  _authStateProvider { get; set; }
    
    protected string ResultStyle { get; set; }
    protected WordModel MainWord{ get; set; }
    protected NotificationType NotificationType { get; set; }
    protected string NotificationText { get; set; }
    protected Notification Notification { get; set; }
    protected bool DisplayNotification { get; set; }
    
    protected Random random = new Random();
    protected List<char> resultWord = new List<char>();
    protected List<char> mixedСharacters = new List<char>();

    protected  override void OnInitialized()
    {
        var user = _sessionService.UserSession;
        MainWord =  _wordService.GetRandomWord(user);
        if (MainWord.WordText != null)
        {
            char[] arrayChars = MainWord.WordText.ToCharArray();
            mixedСharacters = arrayChars.OrderBy(x => random.Next()).ToList();
            resultWord.Clear();
            ResultStyle = "light";
        }
        
    }
    
    
    
    protected void ResetWord()
    {
       OnInitialized();
    }

    protected async void ShowHint()
    {
        NotificationType = NotificationType.Info;
        NotificationText = MainWord.WordText;
        DisplayNotification = true;
        InvokeAsync(StateHasChanged);
        await Task.Delay(3000);
        DisplayNotification = false;
        InvokeAsync(StateHasChanged);
    }
    
    protected void AddCharForResult(char selectChar)
    {
        resultWord.Add(selectChar);
        mixedСharacters.Remove(selectChar);
        ResultStyle = "light";
        if (mixedСharacters?.Count == 0)
        {
            CompareWords();
            InvokeAsync(StateHasChanged);
        }
    }
    
    protected void DeleteCharWithResult(char selectChar)
    {
        resultWord.Remove(selectChar);
        mixedСharacters.Add(selectChar);
        ResultStyle = "light";
           
    }
    
    protected void CompareWords()
    {
        StringBuilder blindWord = new StringBuilder();
           
        for (int i = 0; i < resultWord.Count; i++)
        {
            blindWord.Append(resultWord[i]);
        }
        if (blindWord.ToString().Equals(MainWord.WordText))
        {
            ResultStyle = "success";
        }else
        {
            ResultStyle = "danger";
        }
    }
}