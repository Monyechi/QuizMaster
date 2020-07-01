using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using QuizMaster.Data;
using QuizMaster.Models;

namespace QuizMaster.Controllers
{
    [Authorize(Roles = "Instructor")]
    public class InstructorsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public InstructorsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Instructors
        public async Task<IActionResult> Index()
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);

            var instructor = _context.Instructors.Where(c => c.IdentityUserId == userId).SingleOrDefault();

            if (instructor == null)
            {
                return RedirectToAction("Create");
            }

            return View();
        }

        // GET: Instructors/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var instructor = await _context.Instructors
                .Include(i => i.IdentityUser)
                .FirstOrDefaultAsync(m => m.InstructorId == id);
            if (instructor == null)
            {
                return NotFound();
            }

            return View(instructor);
        }

        // GET: Instructors/Create
        public IActionResult Create()
        {
            ViewData["IdentityUserId"] = new SelectList(_context.Users, "Id", "Id");
            return View();
        }

        // POST: Instructors/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("InstructorId,FirstName,LastName,IdentityUserId")] Instructor instructor)
        {
            if (ModelState.IsValid)
            {
                instructor.InstructorKey = GenerateRandomAlphanumericString();
                _context.Add(instructor);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdentityUserId"] = new SelectList(_context.Users, "Id", "Id", instructor.IdentityUserId);
            return View(instructor);
        }

        // GET: Instructors/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var instructor = await _context.Instructors.FindAsync(id);
            if (instructor == null)
            {
                return NotFound();
            }
            ViewData["IdentityUserId"] = new SelectList(_context.Users, "Id", "Id", instructor.IdentityUserId);
            return View(instructor);
        }

        // POST: Instructors/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("InstructorId,InstructorKey,FirstName,LastName,IdentityUserId")] Instructor instructor)
        {
            if (id != instructor.InstructorId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(instructor);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!InstructorExists(instructor.InstructorId))
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
            ViewData["IdentityUserId"] = new SelectList(_context.Users, "Id", "Id", instructor.IdentityUserId);
            return View(instructor);
        }

        // GET: Instructors/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var instructor = await _context.Instructors
                .Include(i => i.IdentityUser)
                .FirstOrDefaultAsync(m => m.InstructorId == id);
            if (instructor == null)
            {
                return NotFound();
            }

            return View(instructor);
        }

        // POST: Instructors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var instructor = await _context.Instructors.FindAsync(id);
            _context.Instructors.Remove(instructor);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool InstructorExists(int id)
        {
            return _context.Instructors.Any(e => e.InstructorId == id);
        }
        public string GenerateRandomAlphanumericString()
        {
            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var stringChars = new char[20];
            var random = new Random();

            for (int i = 0; i < stringChars.Length; i++)
            {
                stringChars[i] = chars[random.Next(chars.Length)];
            }

            var finalString = new String(stringChars);
            return finalString;
        }
        public async Task<IActionResult> CreateMessage()
        {
           
            return View();
        }
        [HttpPost]
        public IActionResult CreateMessage([Bind("Subject,MessageContent")] Message message)
        {
            //var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            //var instructor = _context.Instructors.Where(c => c.IdentityUserId == userId).SingleOrDefault();

            //message.Reciever = instructor.InstructorName;
            //message.Sender = instructor.DisplayName;
            //_context.Messages.Add(message);
            //_context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Messages()
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var instructor = _context.Instructors.Where(s => s.IdentityUserId == userId).SingleOrDefault();
            var instructorName = instructor.FirstName + " " + instructor.LastName;

            var messages = _context.Messages.Where(m => m.Reciever == instructorName).ToList();

            return View(messages);
        }
        public async Task<IActionResult> ViewStudents()
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var instructor = _context.Instructors.Where(c => c.IdentityUserId == userId).SingleOrDefault();
            var instructorName = instructor.FirstName + " " + instructor.LastName;

            var myStudents = _context.Students.Where(s => s.InstructorName == instructorName);
            return View(myStudents);
        }

    }
}
