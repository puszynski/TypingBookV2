using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using TypingBook.Models;

namespace TypingBook.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Book> Books { get; set; }
        public DbSet<Agreement> Agreements { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //modelBuilder.Entity<Agreement>()
            //    .HasOne(p => p.User)
            //    .WithMany(b => b.Agreements)
            //    .HasForeignKey(p => p.UserId);



            modelBuilder.Entity<Agreement>().HasData
                (
                    new Agreement
                    {
                        ID = 1, 
                        UserId = "34f8fa11-fa68-4c9c-ab2a-0424b0d9319d",                        
                        Description = "Testowa umowa PRZEDŁUŻONA dwumiesięczna: styczeń-luty, wygenerowana jako metoda SeedData",
                        SignedDate = DateTime.Parse("2019-01-01"),
                        From = DateTime.Parse("2019-01-01"),
                        To = DateTime.Parse("2019-02-28"),
                        TerminationDate = null,
                        TerminationType = null
                    },
                    new Agreement
                    {
                        ID = 2,
                        UserId = "34f8fa11-fa68-4c9c-ab2a-0424b0d9319d",
                        Description = "Testowa umowa dwumiesięczna, NIEPRZEDŁUŻONA: luty-marzec, wygenerowana jako metoda SeedData",
                        SignedDate = DateTime.Parse("2019-02-01"),
                        From = DateTime.Parse("2019-02-01"),
                        To = DateTime.Parse("2019-03-31"),
                        TerminationDate = null,
                        TerminationType = null
                    },
                    new Agreement
                    {
                        ID = 3,
                        UserId = "34f8fa11-fa68-4c9c-ab2a-0424b0d9319d",
                        Description = "Testowa umowa dwumiesięczna, RÓWNOLEGŁA - PRZEDŁUŻONA KARNETEM RÓWNOLEGŁYM: maj-czerwiec, wygenerowana jako metoda SeedData",
                        SignedDate = DateTime.Parse("2019-05-01"),
                        From = DateTime.Parse("2019-05-01"),
                        To = DateTime.Parse("2019-06-30"),
                        TerminationDate = null,
                        TerminationType = null
                    },
                    new Agreement
                    {
                        ID = 4,
                        UserId = "34f8fa11-fa68-4c9c-ab2a-0424b0d9319d",
                        Description = "Testowa umowa RÓWNOLEGŁA: kwiecień-lipiec, NIEPRZEDŁUŻONA, wygenerowana jako metoda SeedData",
                        SignedDate = DateTime.Parse("2019-04-01"),
                        From = DateTime.Parse("2019-04-01"),
                        To = DateTime.Parse("2019-07-31"),
                        TerminationDate = null,
                        TerminationType = null
                    }
                );
        }
    }
}



