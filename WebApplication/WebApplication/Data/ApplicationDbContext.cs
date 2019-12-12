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

            builder.Entity<ProjectType>().HasData(
                new ProjectType { Id = 1, Name = "Đồ án cơ sở" },
                new ProjectType { Id = 2, Name = "Đồ án chuyên ngành" },
                new ProjectType { Id = 3, Name = "Đồ án thực tập" },
                new ProjectType { Id = 4, Name = "Đồ án tổng hợp" }
                );
            builder.Entity<Project>().HasData(
              new Project { Id = 1, Title = "Hệ thống quản lý đồ án Hutech", ProjectTypeId = 2, LecturerId = "50717a31-498c-434a-972b-d79c0b9453a7", Description = "", Status = 0 }
              );
            builder.Entity<ProjectMember>().HasData(
                new ProjectMember { ProjectId = 1, StudentId = "dc291a7b-65b1-4a7f-a7c5-5e8dfef5e122" });
                }
    }
}
