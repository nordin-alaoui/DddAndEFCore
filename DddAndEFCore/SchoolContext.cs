using System;

namespace DddAndEFCore;

public class SchoolContext : DbContext
{
    public DbSet<Student> Students { get; set; } = null!;
    public DbSet<Course> Courses { get; set; } = null!;


    public SchoolContext(DbContextOptions<SchoolContext> options) 
        : base(options)
    {
    }
    public void onModelConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Server=localhost;Database=School;User Id=sa;Password=Password123;TrustServerCertificate=true")
            .EnableSensitiveDataLogging()
            .LogTo(Console.WriteLine, LogLevel.Information);
    }

    public void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Student>(x =>
        {
            x.toTable("Student");
            x.HasKey(x => x.Id);
            x.property(x => x.Id).HasColumnName("StudentID");
            x.Property(x => x.Name).HasMaxLength(100).IsRequired();
            // x.Property(x => x.Email).HasMaxLength(100).IsRequired();
            // x.HasMany(x => x.Enrollments).WithOne(x => x.Student).HasForeignKey(x => x.StudentId);

        });

        modelbuilder.Entity<Course>(x =>
        {
            x.toTable("Course");
            x.HasKey(x => x.Id);
             x.Property(x => x.Id).HasColumnName("CourseID");
            x.Property(x => x.Name).HasMaxLength(100).IsRequired();
            // x.HasMany(x => x.Enrollments).WithOne(x => x.Course).HasForeignKey(x => x.CourseId);
        });
            
    }





    
}
