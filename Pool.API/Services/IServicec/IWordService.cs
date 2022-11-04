using Pool.Shared.Models;

namespace Pool.API.Services.IServicec;

public interface IWordService
{
    Task<List<Word>> GetAllUsersWords(Guid userId);
    Task<List<Word>> GetFourRandomWords(Guid userId);
    Task<Word> GetRandomWord(Guid userId);
    Task<bool> DeleteWordById(int id);
    Task<bool> AddWord( Word word);
}