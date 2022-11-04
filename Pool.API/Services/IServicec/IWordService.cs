using Pool.Shared.Models;

namespace Pool.API.Services.IServicec;

public interface IWordService
{
    Task<List<Word>> GetAllUsersWords();
    Task<List<Word>> GetFourRandomWords(Guid userId);
    Task<Word> GetRandomWord(Guid userId);
    Task<bool> DeleteWordById(int id);
}