using Blazored.LocalStorage;

namespace Pool.UI.Services;

public class WordService : HttpServiceBase
{
    public WordService(ILocalStorageService localStorageService) : base(localStorageService)
    {
        _apiControllerName = "word";
    }

    protected override string _apiControllerName { get; set; }
}