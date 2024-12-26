using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace SchollApi;

public class AppDbContext : IdentityDbContext<IdentityUser>
{
    public AppDbContext(DbContextOptions<AppDbContext> options) 
    : base(options)
    {}

    public DbSet<Student> Students { get; set; }

    // protected override void OnModelCreating(ModelBuilder modelBuilder) 
    // {
    //     modelBuilder.Entity<Student>().HasData(
    //         new Student() 
    //         {
    //             Id = 1,
    //             Name = "Maria da Penha",
    //             Email = "mariapenha@email.com",
    //             Age = 32
    //         },
    //         new Student()
    //         {
    //             Id = 2,
    //             Name = "Manoel Bueno",
    //             Email = "manoelbueno@email.com",
    //             Age = 22
    //         } 
    //     );
    // } 
}
