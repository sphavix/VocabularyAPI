using Microsoft.EntityFrameworkCore;
using VocabularyAPI.Models;

namespace VocabularyAPI.RepositoryPattern
{
    public class WordService: IWordService
    {
        private readonly DictionaryDbContext _context;
        public WordService(DictionaryDbContext context)
        {
            _context = context;
        }   

        public List<Word> GetAllWords()
        {
            return _context.Words.ToList();
        }

        public Word GetWordById(int id)
        {
            return _context.Words.FirstOrDefault(x => x.WordId == id);
        }

        public void AddWord(Word word)
        {
            using(var transaction = _context.Database.BeginTransaction())
            {
                //Allows explicit values to be inserted into the identity column of a table
                //_context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT dbo.Words ON;");
                _context.Words.Add(word);
                _context.SaveChanges();
                //_context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT dbo.Words OFF;");
                transaction.Commit();
            }
        }

        public void UpdateWord(Word word)
        {
            using (var transaction = _context.Database.BeginTransaction())
            {
                var model = _context.Words.FirstOrDefault(x => x.WordId == word.WordId);
                if(model != null)
                {
                    model.WordId = word.WordId;
                    model.WordName = word.WordName;
                    model.Definition1 = word.Definition1;
                    model.Definition2 = word.Definition2;
                    model.Definition3 = word.Definition3;
                    model.Sentence = word.Sentence;

                    //Allows explicit values to be inserted into the identity column of a table
                    //_context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT dbo.Words ON;");
                    _context.Words.Update(model);
                    _context.SaveChanges();
                    //_context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT dbo.Words OFF;");
                    transaction.Commit();
                }
                
            }
        }

        public void DeleteWord(int id)
        {
            var word = _context.Words.FirstOrDefault(x => x.WordId == id);
            if(word != null)
            {
                _context.Words.Remove(word);
                _context.SaveChanges();
            }
        }
    }
}
