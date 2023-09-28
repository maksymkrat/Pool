using Pool.Shared.Models;

namespace Pool.API.Repository.IRepository;

public interface IWordRepository
{
    Task<IEnumerable<WordModel>> GetAllUsersWords(Guid userId); 
    Task<IEnumerable<WordModel>> SearchWords( Guid userId, string word); 
    Task<bool> AddWord( WordModel word);
    Task<bool> DeleteById(int id);
    Task<IEnumerable<WordModel>> GetFourRandomWords(Guid userId);
    Task<WordModel> GetRandomWord(Guid userId);
    Task<bool> Update( WordModel word);
    
}