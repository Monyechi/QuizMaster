using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Newtonsoft.Json;
using QuizMaster.Data;
using QuizMaster.Models;

namespace QuizMaster.Controllers
{
    [Authorize(Roles = "Student")]
    public class StudentsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public StudentsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Students
        public async Task<IActionResult> Index()
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);

            var student = _context.Students.Where(c => c.IdentityUserId == userId).SingleOrDefault();

            if (student == null)
            {
                return RedirectToAction("Create");
            }

            return View();
        }

        // GET: Students/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = await _context.Students
                .Include(s => s.IdentityUser)
                .FirstOrDefaultAsync(m => m.StudentId == id);
            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }

        // GET: Students/Create
        public IActionResult Create()
        {
            ViewData["IdentityUserId"] = new SelectList(_context.Users, "Id", "Id");
            return View();
        }

        // POST: Students/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("StudentId,FirstName,LastName,DisplayName,ProfileAvatar,Grade,IdentityUserId")] Student student)
        {
            if (ModelState.IsValid)
            {
                var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
                student.IdentityUserId = userId;
                _context.Add(student);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdentityUserId"] = new SelectList(_context.Users, "Id", "Id", student.IdentityUserId);
            return View(student);
        }

        // GET: Students/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = await _context.Students.FindAsync(id);
            if (student == null)
            {
                return NotFound();
            }
            ViewData["IdentityUserId"] = new SelectList(_context.Users, "Id", "Id", student.IdentityUserId);
            return View(student);
        }

        // POST: Students/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("StudentId,FirstName,LastName,DisplayName,ProfileAvatar,Grade,IdentityUserId")] Student student)
        {
            if (id != student.StudentId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(student);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StudentExists(student.StudentId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdentityUserId"] = new SelectList(_context.Users, "Id", "Id", student.IdentityUserId);
            return View(student);
        }

        // GET: Students/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = await _context.Students
                .Include(s => s.IdentityUser)
                .FirstOrDefaultAsync(m => m.StudentId == id);
            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }

        // POST: Students/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var student = await _context.Students.FindAsync(id);
            _context.Students.Remove(student);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StudentExists(int id)
        {
            return _context.Students.Any(e => e.StudentId == id);
        }
        public IActionResult SearchArticle()
        {
            return View();
        }
        public void GenerateQuestionGN()
        {
            Quiz quiz = new Quiz();

            WebClient client = new WebClient();
            string json = client.DownloadString("https://opentdb.com/api.php?amount=1&category=9&difficulty=hard&type=multiple");
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

            _context.Quizzes.Add(quiz);
            _context.SaveChanges();
        }
        public void GenerateQuestionHistory()
        {
            Quiz quiz = new Quiz();

            WebClient client = new WebClient();
            string json = client.DownloadString("https://opentdb.com/api.php?amount=1&category=23&difficulty=medium&type=multiple");
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

            _context.Quizzes.Add(quiz);
            _context.SaveChanges();
        }
        public void GenerateQuestionScience()
        {
            Quiz quiz = new Quiz();

            WebClient client = new WebClient();
            string json = client.DownloadString("https://opentdb.com/api.php?amount=1&category=17&difficulty=medium&type=multiple");
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

            _context.Quizzes.Add(quiz);
            _context.SaveChanges();
        }
        public void GenerateQuestionMath()
        {
            Quiz quiz = new Quiz();

            WebClient client = new WebClient();
            string json = client.DownloadString("https://opentdb.com/api.php?amount=1&category=19&difficulty=medium&type=multiple");
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

            _context.Quizzes.Add(quiz);
            _context.SaveChanges();
        }
        public void GenerateQuestionRandom()
        {
            Quiz randCat = new Quiz();

            WebClient client = new WebClient();
            string json = client.DownloadString("https://opentdb.com/api.php?amount=1&difficulty=medium&type=multiple");
            dynamic dobj = JsonConvert.DeserializeObject<dynamic>(json);

            randCat.Question = dobj.results[0].question;
            randCat.Category = dobj.results[0].category;
            randCat.Difficulty = dobj.results[0].Difficulty;
            randCat.CorrectAnswer = dobj.results[0].correct_answer;
            randCat.WrongAnswer1 = dobj.results[0].incorrect_answers[0];
            randCat.WrongAnswer2 = dobj.results[0].incorrect_answers[1];
            randCat.WrongAnswer3 = dobj.results[0].incorrect_answers[2];

            string[] answers = new string[4];
            answers[0] = randCat.CorrectAnswer;
            answers[1] = randCat.WrongAnswer1;
            answers[2] = randCat.WrongAnswer2;
            answers[3] = randCat.WrongAnswer3;

            var shuffledArray = ShuffleArray(answers);
            randCat.Answer1 = shuffledArray[0];
            randCat.Answer2 = shuffledArray[1];
            randCat.Answer3 = shuffledArray[2];
            randCat.Answer4 = shuffledArray[3];

            _context.Quizzes.Add(randCat);
            _context.SaveChanges();
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

 
        public void GenerateQuizQuestions()
        {
            for (int i = 0; i < 50; i++)
            {
                GenerateQuestionGN();
                GenerateQuestionHistory();
                GenerateQuestionMath();
                GenerateQuestionRandom();
                GenerateQuestionScience();
            }

            var allQuizzes = _context.Quizzes.ToList();
        }   

        public void compareAnswer1(string answer)
        {

        }
        public IActionResult ScienceEasy()
        {
            

            return View();
        }
        public IActionResult ScienceMedium()
        {
            

            return View();
        }
        public IActionResult ScienceHard()
        {


            return View();
        }
        public IActionResult MathEasy()
        {
            

            return View();
        }
        public IActionResult MathMedium()
        {
            

            return View();
        }
        public IActionResult MathHard()
        {


            return View();
        }
        public IActionResult HistoryEasy()
        {
            

            return View();
        }
        public IActionResult HistoryMedium()
        {
            

            return View();
        }
        public IActionResult HistoryHard()
        {


            return View();
        }

    }
}
