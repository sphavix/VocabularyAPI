using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;
using VocabularyAPI.Models;
using VocabularyAPI.RepositoryPattern;

namespace VocabularyWeb.Controllers
{
    public class WordsController : Controller
    {
        //private readonly IWordService _service;

        HttpClientHandler handler = new HttpClientHandler();
        public WordsController(/*IWordService service*/)
        {
            //_service = service;
            handler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) =>
            {
                return true;
            };
        }
        Word word = new Word();
        List<Word> _words = new List<Word>();


        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<List<Word>> GetAllWords()
        {
            //var _words = _service.GetAllWords();
            _words = new List<Word>();
            using(var httpClient = new HttpClient(handler))
            {
                using(var response = await httpClient.GetAsync("https://localhost:7097/words"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    _words = JsonConvert.DeserializeObject<List<Word>>(apiResponse);
                }
            }
            return _words;
        }

        [HttpGet]
        public async Task<Word> GetWordById(int wordId)
        {
            //var _word = _service.GetWordById(wordId);
            word = new Word();
            using (var httpClient = new HttpClient(handler))
            {
                using (var response = await httpClient.GetAsync("https://localhost:7097/words" + wordId))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    word = JsonConvert.DeserializeObject<Word>(apiResponse);
                }
            }
            return word;
        }

        [HttpPost]
        public async Task<Word> AddOrEditWord(Word model)
        {
            var word = new Word();
            using(var client = new HttpClient(handler))
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");

                using(var response = await client.PostAsync("https://localhost:7097/words", content))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    word = JsonConvert.DeserializeObject<Word>(apiResponse);
                }
            }
            return word;
        }

        [HttpDelete]
        public async Task<string> DeleteWord(int wordId)
        {
            string message = "";

            using (var httpClient = new HttpClient(handler))
            {
                using (var response = await httpClient.DeleteAsync("https://localhost:7097/words" + wordId))
                {
                   message = await response.Content.ReadAsStringAsync();
                }
            }
            return message;
        }
    }
}
