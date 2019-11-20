using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using TypingBook.Enums;
using TypingBook.Models;

namespace TypingBook.Data
{nude
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Book> Books { get; set; }
        public DbSet<UserData> UserData { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
            // seed data
            modelBuilder.Entity<Book>().HasData
                (
                    new Book
                    {
                        Id = 1,
                        Title = "testTitle",
                        Content = "testContent",
                        Authors = "testAuthors",
                        Genre = (int)EBookGenre.History,
                        Rate = 5,
                        ReleaseDate = DateTime.Now
                    }
                );
        }
    }
}



