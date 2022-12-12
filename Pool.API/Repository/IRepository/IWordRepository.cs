using Pool.Shared.Models;

namespace Pool.API.Repository.IRepository;

public interface IWordRepository
{
    Task<List<WordModel>> GetAllUsersWords(Guid userId); 
    Task<List<WordModel>> SearchWords(string word, Guid userId); 
    Task<bool> AddWord( WordModel word);
    Task<bool> DeleteById(int id);
    Task<List<WordModel>> GetFourRandomWords(Guid userId);
    Task<WordModel> GetRandomWord(Guid userId);
    Task<bool> Update( WordModel word);
    
}