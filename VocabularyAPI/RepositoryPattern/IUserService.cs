using VocabularyAPI.Models;

namespace VocabularyAPI.RepositoryPattern
{
    public interface IUserService
    {
        User GetUser(UserLogin model);
    }
}
