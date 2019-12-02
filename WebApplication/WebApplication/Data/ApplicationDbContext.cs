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

        public virtual DbSet<ProjectType> ProjectTypes { get; set; }

        public virtual DbSet<Project> Projects { get; set; }

        public virtual DbSet<ProjectMember> ProjectMembers { get; set; }

        public virtual DbSet<ProjectSchedule> ProjectSchedules { get; set; }

        public virtual DbSet<ProjectScheduleReport> ProjectScheduleReports { get; set; }

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
            builder.Entity<ProjectMember>()
                .HasKey(pm => new { pm.ProjectId, pm.StudentId });
            builder.Entity<ProjectMember>()
                .HasOne(pm => pm.Project)
                .WithMany(p => p.ProjectMembers)
                .HasForeignKey(pm => pm.ProjectId);
            builder.Entity<ProjectMember>()
                .HasOne(pm => pm.Student)
                .WithMany(s => s.ProjectMembers)
                .HasForeignKey(pm => pm.StudentId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
