using VocabularyAPI.Models;

namespace VocabularyAPI.RepositoryPattern
{
    public interface ISynonymService
    {
        List<Synonym> GetAllSynonyms();
        Synonym GetSynonymById(int id);
        void AddSynonym(Synonym synonym);
        void UpdateSynonym(Synonym synonym);
        void DeleteSynonym(int id);
    }
}
