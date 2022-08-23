using VocabularyAPI.Models;

namespace VocabularyAPI.RepositoryPattern
{
    public interface IWordService
    {
        List<Word> GetAllWords();
        Word GetWordById(int id);
        void AddWord(Word word);
        void UpdateWord(Word word);
        void DeleteWord(int id);
    }
}
