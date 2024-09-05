 using Library_Management_System.Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Metrics;

namespace Library_Management_System.Data.Context;
public class LibraryDbContext : IdentityDbContext<User>
{
    public LibraryDbContext(DbContextOptions<LibraryDbContext> options)
        : base(options)
    {
    }

    public DbSet<Book> Books { get; set; }
    public DbSet<LibraryNotification> LibarianMessages { get; set; }
    public DbSet<Borrowing> Borrowings { get; set; }
    public DbSet<User> Users { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

        base.OnModelCreating(modelBuilder);

      
       // //Seeding Country Master Data using HasData method
       // modelBuilder.Entity<IdentityRole>().HasData(
       //  new IdentityRole
       //  {
       //      Id = Guid.NewGuid().ToString(),
       //      Name = "Libarian",
       //      NormalizedName = "LIBARIAN"
       //  },
       //    new IdentityRole
       //    {
       //        Id = Guid.NewGuid().ToString(),
       //        Name = "User",
       //        NormalizedName = "USER"
       //    }
       //);

        modelBuilder.Entity<LibraryNotification>(entity =>
        {
            entity.HasKey(e => e.Id);

            entity.HasOne(e => e.User)
                .WithMany(b => b.Notifications)
                .HasForeignKey(e => e.UserId)
                .OnDelete(DeleteBehavior.NoAction);
        });
    }
}
