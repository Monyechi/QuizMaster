﻿using System;
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
        [HttpGet]
        public IActionResult Message()
        {
            return View();         
           
        }
        public IActionResult ViewMessages()
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var student = _context.Students.Where(s => s.IdentityUserId == userId).SingleOrDefault();
            var messages = _context.Messages.Where(m => m.Reciever == student.DisplayName).ToList();
            return View(messages);         
           
        }
        [HttpPost]
        public IActionResult Message([Bind("Subject,MessageContent")] Message message)
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var student = _context.Students.Where(c => c.IdentityUserId == userId).SingleOrDefault();
           
            message.Reciever = student.InstructorName;
            message.Sender = student.DisplayName;
            _context.Messages.Add(message);
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        public IActionResult SelectInstructor()
        {
            var instructors = _context.Instructors.ToList();

            return View(instructors);
        }
        public IActionResult ChooseInstructor(string Id)
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var student = _context.Students.Where(c => c.IdentityUserId == userId).SingleOrDefault();

            var instructor = _context.Instructors.Where(c => c.InstructorKey == Id).SingleOrDefault();
            var instructorName = instructor.FirstName + " " + instructor.LastName;
            student.InstructorName = instructorName;
            _context.SaveChanges();

            return View(instructor);
        }
        public string GenerateRandomAlphanumericString()
        {
            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var stringChars = new char[50];
            var random = new Random();

            for (int i = 0; i < stringChars.Length; i++)
            {
                stringChars[i] = chars[random.Next(chars.Length)];
            }

            var finalString = new String(stringChars);
            return finalString;
        }

    }
}
