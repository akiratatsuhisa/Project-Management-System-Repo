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

            builder.Entity<Faculty>().HasData(
                new Faculty { Id = 1, Name = "Công nghệ thông tin" }
                );
            builder.Entity<SpecializedFaculty>().HasData(
                new SpecializedFaculty { Id = 1, FacultyId = 1, Name = "Công nghệ phần mềm"},
                new SpecializedFaculty { Id = 2, FacultyId = 1, Name = "Hệ thống thông tin"},
                new SpecializedFaculty { Id = 3, FacultyId = 1, Name = "An toàn thông tin"}
                );
            builder.Entity<ProjectType>().HasData(
                new ProjectType { Id = 1, Name = "Đồ án cơ sở"},
                new ProjectType { Id = 2, Name = "Đồ án chuyên ngành"},
                new ProjectType { Id = 3, Name = "Đồ án thực tập"},
                new ProjectType { Id = 4, Name = "Đồ án tổng hợp"}
                );
        }
    }
}
