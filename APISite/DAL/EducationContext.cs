using APISite.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace APISite.DAL
{
    public class EducationContext : DbContext
    {
        public DbSet<Course> Courses { get; set; } = null!;
        public DbSet<Student> Students { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Student>()
                .HasMany(s => s.Courses)
                .WithMany(c => c.Students);
        }
        public EducationContext(DbContextOptions options) : base(options)
        {

        }
    }
}