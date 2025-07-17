using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using CQRS_University_System.Domain.Entities;
using CQRS_University_System.Domain.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CQRS_University_System.Infrastructure.Data
{
    public class AppDbContext : IdentityDbContext<ApplicationUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Student> Students { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<StudentCourses> StudentCourses { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Apply OnDelete(DeleteBehavior.Restrict) to all foreign key relationships
            foreach (var forgienKey in builder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                forgienKey.DeleteBehavior = DeleteBehavior.Restrict;
            }


            builder.Entity<StudentCourses>()
                .HasKey(st => new { st.StudentId, st.CourseId });

            builder.Entity<StudentCourses>()
                .HasOne(st => st.Student)
                .WithMany(st => st.StudentCourses)
                .HasForeignKey(st => st.StudentId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<StudentCourses>()
                .HasOne(st => st.Course)
                .WithMany(st => st.StudentCourses)
                .HasForeignKey(st => st.CourseId)
                .OnDelete(DeleteBehavior.Cascade);


            builder.Entity<Department>().HasData(new List<Department>
            {
                new Department { Id = 1, Name = "Software" },
                new Department { Id = 2, Name = "Blockchain" },
                new Department { Id = 3, Name = "Data Science" },
                new Department { Id = 4, Name = "Cyber Security" },
                new Department { Id = 5, Name = "AI & Machine Learning" },
            });


            // Customize Special Schema for Identity Tables

            builder.Entity<ApplicationUser>().ToTable("Users", "security");
            builder.Entity<IdentityRole>().ToTable("Roles", "security");
            builder.Entity<IdentityUserRole<string>>().ToTable("UserRoles", "security");
            builder.Entity<IdentityUserClaim<string>>().ToTable("UserClaims", "security");
            builder.Entity<IdentityUserLogin<string>>().ToTable("UserLogins", "security");
            builder.Entity<IdentityRoleClaim<string>>().ToTable("RoleClaims", "security");
            builder.Entity<IdentityUserToken<string>>().ToTable("UserTokens", "security");
        }

    }
}
