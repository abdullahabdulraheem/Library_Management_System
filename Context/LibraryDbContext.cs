using Microsoft.EntityFrameworkCore;
using Library_Management_System.Data;
using System.Reflection;
// using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
// using System.Reflection;
// using RazaBookingSystem.Data;
// using Microsoft.AspNetCore.Identity;
namespace Library_Management_System.Context
{
    public class LibraryDbContext(DbContextOptions<LibraryDbContext> options) : IdentityDbContext(options)
    {

        protected override void OnModelCreating(ModelBuilder builder)
        {
             base.OnModelCreating(builder);
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }

        public DbSet<Book> Books { get; set; }
        public DbSet<Borrowing> Borrowings { get; set; }
    }
}
