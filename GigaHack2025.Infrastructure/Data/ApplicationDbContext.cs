using Microsoft.EntityFrameworkCore;
using GigaHack2025.Core.Entities;
using GigaHack2025.Core.Enums;

namespace GigaHack2025.Infrastructure.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    public DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // User entity configuration
        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id);

            entity.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(50);

            entity.Property(e => e.Surname)
                .IsRequired()
                .HasMaxLength(50);

            entity.Property(e => e.Email)
                .IsRequired()
                .HasMaxLength(100);

            entity.Property(e => e.PhoneNumber)
                .IsRequired()
                .HasMaxLength(20);

            entity.Property(e => e.CompanyCode)
                .IsRequired()
                .HasMaxLength(13);

            entity.Property(e => e.PasswordHash)
                .IsRequired();

            // Configure Role enum to be stored as string
            entity.Property(e => e.Role)
                .HasConversion<string>()
                .IsRequired();

            // Create unique index on email
            entity.HasIndex(e => e.Email).IsUnique();

            // Create index on role for faster queries
            entity.HasIndex(e => e.Role);
        });

        // No seed data here - create admin through API
    }
}
