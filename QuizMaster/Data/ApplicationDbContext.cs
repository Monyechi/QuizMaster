﻿using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using QuizMaster.Models;

namespace QuizMaster.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<IdentityRole>()
            .HasData(
           new IdentityRole
           {
               Name = "Student",
               NormalizedName = "STUDENT"

           },

            new IdentityRole
            {
                Name = "Instructor",
                NormalizedName = "INSTRUCTOR"
            }
            );
            
        }

        public DbSet<Student> Students { get; set; }
        public DbSet<Instructor> Instructors { get; set; }
        public DbSet<Quiz> Quizzes { get; set; }
        public DbSet<Message> Messages { get; set; }
    }
}
