using Pool.Shared.Models;

namespace Pool.API.Services.IServicec;

public interface IWordService
{
    Task<List<Word>> GetAllUsersWords();
}