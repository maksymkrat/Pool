using Pool.API.Repository.IRepository;
using Pool.API.Services.IServicec;
using Pool.Shared.Models;

namespace Pool.API.Services;

public class WordService : IWordService
{
    private readonly IWordRepository _wordRepository;
    
    public WordService(IWordRepository wordRepository)
    {
        _wordRepository = wordRepository;
    }

    public Task<List<Word>> GetAllUsersWords()
    {
        return _wordRepository.GetAllUsersWords();
    }
}