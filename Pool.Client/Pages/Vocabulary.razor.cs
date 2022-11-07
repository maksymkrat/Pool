using Microsoft.AspNetCore.Components;
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

    protected List<Word> words = new List<Word>();

    protected List<Word> Words
    {
        get => words;
        set
        {
            words = value;
            InvokeAsync(StateHasChanged);
        }
    }

    protected string newWord;
    protected string newTranslate;
    protected string wordTranslation;
    protected string WordForTranslation;
    protected DeleteConfiraation DeleteConfiramtion { get; set; }
    protected UpdateWord UpdatedWord { get; set; }
    protected Word WordToBeDeleted { get; set; }
    protected Word WordToBeUpdated { get; set; }
    protected readonly Guid userId = new Guid("23A2DCB7-38B5-44B4-85E9-9E6AF7F4646F");


    protected async override Task OnInitializedAsync()
    {
         //await base.OnInitializedAsync();
        UpdateWords();
    }

    protected async void UpdateWords()
    {
        // var userId = _accountService.UserSession.Id;
        Words = await _wordService.GetAllWords(userId); //hardcode
        InvokeAsync(StateHasChanged);
    }

    protected async void AddWord()
    {
        if (!string.IsNullOrWhiteSpace(newWord) && !String.IsNullOrWhiteSpace(newTranslate))
        {
            var result = await _wordService.AddWord(new Word()
            {
                WordText = newWord.ToLower(),
                Translation = newTranslate.ToLower(),
                User_id = userId
            });

            newWord = string.Empty;
            newTranslate = string.Empty;
            wordTranslation = string.Empty;
            if (result)
            {
                UpdateWords();
                InvokeAsync(StateHasChanged);
            }
            else
            {
                //  do not add word
            }
        }
    }

    protected void TranslateWord()
    {
        _logger.LogInformation($"{DateTime.UtcNow.ToLongTimeString()} method: TranslateWord");
        // if (!string.IsNullOrWhiteSpace(newWord))
        // {
        //     translater = _business.Translate(newWord);
        //     wordTranslation = translater.Target.Text;
        //     newTranslate = translater.Target.Text;
        //     WordForTranslation = translater.Source.Text;
        // }
    }

    protected void DeleteWord(Word word)
    {
        WordToBeDeleted = word;
        DeleteConfiramtion.Show();
    }

    protected void UpdateWord(Word word)
    {
        WordToBeUpdated = word;
        UpdatedWord.Show();
    }

    protected void ConfirmDelete()
    {
         _wordService.DeleteById(WordToBeDeleted.Id);
         DeleteConfiramtion.Hide();
        UpdateWords();
        InvokeAsync(StateHasChanged);
        WordToBeDeleted = null;
    }

    protected void ConfirmUpdate()
    {
        
    }

    protected void onCancel()
    {
        UpdatedWord.Hide();
        DeleteConfiramtion.Hide();
        WordToBeDeleted = null;
        WordToBeUpdated = null;
    }

    protected void SayWord(Word word)
    {
        // _business.Speech(word.WordText);
    }
}