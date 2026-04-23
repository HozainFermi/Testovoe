using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using WebApplication1.Data.Entities;

namespace WebApplication1.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Education> Educations { get; set; }
        public DbSet<Promotion> Promotions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.StuffNumber).IsRequired();
                entity.Property(e => e.FullName).IsRequired();
                entity.Property(e => e.BirthDate).IsRequired();
                entity.Property(e => e.HireDate).IsRequired();
                entity.Property(e => e.FiredDate).IsRequired(false); 

                entity.HasOne(e => e.Education)
                    .WithMany(x => x.Employees)
                    .HasForeignKey(e => e.EducationId)
                    .OnDelete(DeleteBehavior.Restrict); 

                entity.HasOne(e => e.Department)
                    .WithMany(d => d.Employees)
                    .HasForeignKey(e => e.DepartmentId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.Property(g => g.CreatedAt)
                    .HasDefaultValueSql("GETUTCDATE()"); 
            });

            modelBuilder.Entity<Department>(entity =>
            {
                entity.HasKey(d => d.Id);
                entity.Property(d => d.DepartmentName).IsRequired();
                entity.Property(d => d.CreatedAt).HasDefaultValueSql("GETUTCDATE()");
            });

            modelBuilder.Entity<Education>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.EducationLevel)
                    .IsRequired()
                    .HasConversion(
                        v => v.ToString(),
                        v => (EducationLevel)Enum.Parse(typeof(EducationLevel), v));
                entity.Property(e => e.CreatedAt).HasDefaultValueSql("GETUTCDATE()");
            });

            modelBuilder.Entity<Promotion>(entity =>
            {
                entity.HasKey(p => p.Id);
                entity.Property(p => p.IncreasingPercent).IsRequired();
                entity.Property(p => p.CreatedAt).HasDefaultValueSql("GETUTCDATE()");

                entity.HasOne(p => p.Employee)
                    .WithOne(e => e.Promotion) 
                    .HasForeignKey<Promotion>(p => p.EmployeeId)
                    .OnDelete(DeleteBehavior.Cascade);
            });
        }

    }
}
