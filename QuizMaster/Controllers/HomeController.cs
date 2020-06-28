using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using QuizMaster.Data;
using QuizMaster.Models;

namespace QuizMaster.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
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

        public IActionResult Animals()
        {
            return View();
        }
        public IActionResult History()
        {
            return View();
        }
        public IActionResult Math()
        {
            return View();
        }
        public IActionResult Science()
        {
            
            return View();
        }

        [HttpPost]
        public IActionResult Science(string selectedAnswwer, Quiz quiz)
        {
            selectedAnswwer.ToString();
            if (selectedAnswwer == quiz.CorrectAnswer)
            {
                return RedirectToAction(nameof(Correct));
            }
            else
            {
                return RedirectToAction(nameof(Wrong));
            }

        }

        public void GetQuiz()
        {
            Quiz quiz = new Quiz();

            WebClient client = new WebClient();
            string json = client.DownloadString("https://opentdb.com/api.php?amount=1&category=17&difficulty=hard&type=multiple");
            dynamic dobj = JsonConvert.DeserializeObject<dynamic>(json);

            quiz.Question = dobj.results[0].question;
            quiz.Category = dobj.results[0].category;
            quiz.Difficulty = dobj.results[0].Difficulty;
            quiz.CorrectAnswer = dobj.results[0].correct_answer;
            quiz.WrongAnswer1 = dobj.results[0].incorrect_answers[0];
            quiz.WrongAnswer2 = dobj.results[0].incorrect_answers[1];
            quiz.WrongAnswer3 = dobj.results[0].incorrect_answers[2];

            string[] answers = new string[4];
            answers[0] = quiz.CorrectAnswer;
            answers[1] = quiz.WrongAnswer1;
            answers[2] = quiz.WrongAnswer2;
            answers[3] = quiz.WrongAnswer3;

            var shuffledArray = ShuffleArray(answers);
            quiz.Answer1 = shuffledArray[0];
            quiz.Answer2 = shuffledArray[1];
            quiz.Answer3 = shuffledArray[2];
            quiz.Answer4 = shuffledArray[3];

            

            
        }
        public static string[] ShuffleArray(string[] array)
        {
            int n = array.Length;
            Random rand = new Random();

            for (int i = 0; i < n; i++)
            {
                swap(array, i, i + rand.Next(n - i));
            }
            return array;
        }
        public static void swap(string[] array, int a, int b)
        {
            string temp = array[a];
            array[a] = array[b];
            array[b] = temp;
        }
        public IActionResult Correct()
        {
            return View();
        }
        public IActionResult Wrong()
        {
            return View();
        }

    }
}
