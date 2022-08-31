using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using VocabularyWeb.Models;

namespace VocabularyWeb.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        HttpClientHandler handler = new HttpClientHandler();
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
            handler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) =>
            {
                return true;
            };
        }
        
        public IActionResult Index()
        {
            IEnumerable<WordsViewModel> wordlist;
            var httpClient = new HttpClient(handler);
            HttpResponseMessage response = httpClient.GetAsync("https://localhost:7097/words").Result;
            wordlist = response.Content.ReadAsAsync<IEnumerable<WordsViewModel>>().Result;
            ViewBag.Words = wordlist;
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}