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
    public DbSet<FarmerProfile> FarmerProfiles { get; set; }

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

        // FarmerProfile entity configuration
        // In your OnModelCreating method, update the FarmerProfile configuration:
        modelBuilder.Entity<FarmerProfile>(entity =>
        {
            entity.HasKey(e => e.Id);

            // Required fields
            entity.Property(e => e.UserId).IsRequired();
            entity.Property(e => e.CreatedAt).IsRequired();
            entity.Property(e => e.UpdatedAt).IsRequired();
            entity.Property(e => e.IsActive).IsRequired();

            // All other fields are now optional (nullable)
            entity.Property(e => e.FullName).HasMaxLength(100);
            entity.Property(e => e.CompanyName).HasMaxLength(100);
            entity.Property(e => e.LegalForm).HasConversion<string>();
            entity.Property(e => e.CompanyIdno).HasMaxLength(13);
            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.Phone).HasMaxLength(20);
            entity.Property(e => e.Address).HasMaxLength(200);

            entity.Property(e => e.RepFirstName).HasMaxLength(50);
            entity.Property(e => e.RepLastName).HasMaxLength(50);
            entity.Property(e => e.RepGender).HasConversion<string>();
            entity.Property(e => e.RepIdNumber).HasMaxLength(20);

            entity.Property(e => e.TotalFarmland).HasPrecision(10, 2);
            entity.Property(e => e.AnnualIncome).HasPrecision(15, 2);
            entity.Property(e => e.AnnualExpenses).HasPrecision(15, 2);

            entity.Property(e => e.SubsidiesReceived).HasMaxLength(500);
            entity.Property(e => e.Notes).HasMaxLength(2000);

            // Relationships
            entity.HasOne(e => e.User)
                  .WithMany()
                  .HasForeignKey(e => e.UserId)
                  .OnDelete(DeleteBehavior.Cascade);

            // Indexes
            entity.HasIndex(e => e.UserId).IsUnique();
        });

    }
}
