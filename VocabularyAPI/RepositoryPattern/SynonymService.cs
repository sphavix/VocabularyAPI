using Microsoft.EntityFrameworkCore;
using VocabularyAPI.Models;

namespace VocabularyAPI.RepositoryPattern
{
    public class SynonymService : ISynonymService
    {
        private readonly DictionaryDbContext _context;
        public SynonymService(DictionaryDbContext context)
        {
            _context = context;
        }

        public List<Synonym> GetAllSynonyms()
        {
            return _context.Synonyms.ToList();
        }

        public Synonym GetSynonymById(int id)
        {
            return _context.Synonyms.FirstOrDefault(x => x.SynonymId == id);
        }

        public void AddSynonym(Synonym synonym)
        {
            using (var transaction = _context.Database.BeginTransaction())
            {
                //Allows explicit values to be inserted into the identity column of a table
                _context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT dbo.Words ON;");
                _context.Synonyms.Add(synonym);
                _context.SaveChanges();
                _context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT dbo.Words OFF;");
                transaction.Commit();
            }
        }

        public void UpdateSynonym(Synonym synonym)
        {
            using (var transaction = _context.Database.BeginTransaction())
            {
                var model = _context.Synonyms.FirstOrDefault(x => x.SynonymId == synonym.SynonymId);
                if (model != null)
                {
                    model.SynonymId = synonym.SynonymId;
                    model.SynonymName = synonym.SynonymName;
                    model.Definition1 = synonym.Definition1;
                    model.Definition2 = synonym.Definition2;
                    model.Definition3 = synonym.Definition3;
                    model.Sentence = synonym.Sentence;

                    //Allows explicit values to be inserted into the identity column of a table
                    _context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT dbo.Words ON;");
                    _context.Synonyms.Update(model);
                    _context.SaveChanges();
                    _context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT dbo.Words OFF;");
                    transaction.Commit();
                }

            }
        }

        public void DeleteSynonym(int id)
        {
            var synonym = _context.Synonyms.FirstOrDefault(x => x.SynonymId == id);
            if (synonym != null)
            {
                _context.Synonyms.Remove(synonym);
                _context.SaveChanges();
            }
        }
    }
}
