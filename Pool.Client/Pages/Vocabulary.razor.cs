using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Pool.Client.Authentication;
using Pool.Client.Components;
using Pool.Client.Services;
using Pool.Shared.Models;
using Pool.Shared.Models.DeserializeTranslation;

namespace Pool.Client.Pages;

public class Vocabulary_razor : ComponentBase
{
    [Inject] private AccountService _accountService { get; set; }
    [Inject] private ILogger<Vocabulary> _logger { get; set; }
    [Inject] private WordService _wordService { get; set; }
    [Inject] private SpeechService _speechService { get; set; }
    [Inject] private AuthenticationStateProvider  _authStateProvider { get; set; }
    [Inject] private SessionService _sessionService { get; set; }



    private List<WordModel> _words = new List<WordModel>();

    protected List<WordModel> Words
    {
        get => _words;
        set
        {
            _words = value;
            InvokeAsync(StateHasChanged);
        }
    }

    protected string NewWord{ get; set; }
    protected string NewTranslate{ get; set; }
    protected string WordTranslation{ get; set; }
    protected string WordForTranslation{ get; set; }
    protected Translator Translator{ get; set; }
    protected DeleteConfiraation DeleteConfirmation { get; set; }
    protected UpdateWord UpdatedWord { get; set; }
    protected WordModel WordToBeDeleted { get; set; }
    protected WordModel WordToBeUpdated { get; set; }


    protected async override Task OnInitializedAsync()
    {
        await UpdateWords();
    }
    
    protected async Task UpdateWords()
    {
         var user = _sessionService.UserSession;
        Words = await _wordService.GetAllWords(user); 
        
        InvokeAsync(StateHasChanged);
    }

    protected async void AddWord()
    {
        if (!string.IsNullOrWhiteSpace(NewWord) && !String.IsNullOrWhiteSpace(NewTranslate))
        {
            var result = await _wordService.AddWord(new WordModel()
            {
                WordText = NewWord.ToLower(),
                Translation = NewTranslate.ToLower(),
                User_Id = _sessionService.UserSession.Id
            });

            NewWord = string.Empty;
            NewTranslate = string.Empty;
            WordTranslation = string.Empty;
            WordForTranslation = string.Empty;
            if (result)
            {
               await UpdateWords();
                InvokeAsync(StateHasChanged);
            }
            else
            {
                //  do not add word
            }
        }
    }

    protected async Task TranslateWord()
    {
        _logger.LogInformation($"{DateTime.UtcNow.ToLongTimeString()} method: TranslateWord");
        if (!string.IsNullOrWhiteSpace(NewWord))
        {
            Translator =  await _wordService.Translate(NewWord);
            WordTranslation = Translator.Target.Text;
            NewTranslate = Translator.Target.Text;
            WordForTranslation = Translator.Source.Text;
        }
    }

    protected void DeleteWord(WordModel word)
    {
        WordToBeDeleted = word;
        DeleteConfirmation.Show();
    }

    protected void UpdateWord(WordModel word)
    {
        WordToBeUpdated = (WordModel) word.Clone();
        UpdatedWord.Show();
    }

    protected async void ConfirmDelete()
    {
        await _wordService.DeleteById(WordToBeDeleted.Id);
        DeleteConfirmation.Hide();
        await UpdateWords();
        InvokeAsync(StateHasChanged);
        WordToBeDeleted = null;
    }

    protected async Task ConfirmUpdate()
    {
        _wordService.UpdateWord(WordToBeUpdated);

        UpdatedWord.Hide();
       await UpdateWords();
        WordToBeUpdated = null;
        InvokeAsync(StateHasChanged);
    }

    protected void onCancel()
    {
        UpdatedWord.Hide();
        DeleteConfirmation.Hide();
        WordToBeDeleted = null;
        WordToBeUpdated = null;
    }

    protected void SayWord(WordModel word)
    {
         _speechService.Speech(word.WordText);
    }

    protected async Task SearchWord(string word)
    {
        if (!string.IsNullOrEmpty(word))
        {
            Words = await _wordService.SearchWords(word, _sessionService.UserSession.Id);
        }
        else
        {
          await  UpdateWords();
        }
    }
    protected async Task SearchTranslatedWord(string word)
    {
        if (!string.IsNullOrEmpty(word))
        {
            Words = await _wordService.SearchTranslatedWords(word, _sessionService.UserSession.Id);
        }
        else
        {
          await  UpdateWords();
        }
    }
}