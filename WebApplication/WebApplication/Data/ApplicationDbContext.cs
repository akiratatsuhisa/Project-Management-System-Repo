using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebApplication.Models;

namespace WebApplication.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public virtual DbSet<Faculty> Faculties { get; set; }

        public virtual DbSet<SpecializedFaculty> SpecializedFaculties { get; set; }

        public virtual DbSet<Student> Students { get; set; }

        public virtual DbSet<Lecturer> Lecturers { get; set; }


        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<ApplicationUser>()
                .HasOne(u => u.Lecturer)
                .WithOne(l => l.ApplicationUser);
            builder.Entity<ApplicationUser>()
                .HasOne(u => u.Student)
                .WithOne(s => s.ApplicationUser);
            builder.Entity<Student>()
                .HasIndex(s => s.StudentCode)
                .IsUnique();
            base.OnModelCreating(builder);

        }
    }
}
