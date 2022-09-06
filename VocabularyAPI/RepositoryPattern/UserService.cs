using VocabularyAPI.Models;

namespace VocabularyAPI.RepositoryPattern
{
    public class UserService : IUserService
    {
        private readonly DictionaryDbContext _contxt;

        public UserService(DictionaryDbContext contxt)
        {
            _contxt = contxt;
        }
        public User GetUser(UserLogin model)
        {
            var user = _contxt.Users.FirstOrDefault(u => u.Username.Equals(model.Username, StringComparison.OrdinalIgnoreCase)
                           && u.Password.Equals(model.Password));
            return user;
        }
    }
}
