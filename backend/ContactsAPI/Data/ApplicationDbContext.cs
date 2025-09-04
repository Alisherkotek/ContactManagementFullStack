using Microsoft.EntityFrameworkCore;
using ContactsAPI.Models;

namespace ContactsAPI.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Contact> Contacts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Contact>(entity =>
            {
                entity.HasKey(e => e.Id);
                
                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(100);
                
                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(100);
                
                entity.Property(e => e.PhoneNumber)
                    .IsRequired()
                    .HasMaxLength(20);
                
                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(255);
                
                entity.HasIndex(e => e.Email)
                    .IsUnique();
                
                entity.Property(e => e.CreatedAt)
                    .HasDefaultValueSql("GETUTCDATE()");
            });

            // Initial data seeded
            modelBuilder.Entity<Contact>().HasData(
                new Contact
                {
                    Id = 1,
                    FirstName = "Alisher",
                    LastName = "Konysbekov",
                    PhoneNumber = "+77082780095",
                    Email = "konysbek8@gmail.com",
                    CreatedAt = DateTime.UtcNow
                },
                new Contact
                {
                    Id = 2,
                    FirstName = "Assel",
                    LastName = "Happiness",
                    PhoneNumber = "+123456789",
                    Email = "assel.happiness@example.com",
                    CreatedAt = DateTime.UtcNow
                },
                new Contact
                {
                    Id = 3,
                    FirstName = "Data",
                    LastName = "Matrix",
                    PhoneNumber = "+1122334455",
                    Email = "data.matrix@example.com",
                    CreatedAt = DateTime.UtcNow
                }
            );
        }
    }
}