using Microsoft.EntityFrameworkCore;
using RentEase.API.Models;
using RentEase.API.Services;

namespace RentEase.API.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Property> Properties { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<SavedProperty> SavedProperties { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // ── User — unique email ──
            modelBuilder.Entity<User>()
                .HasIndex(u => u.Email)
                .IsUnique();

            // ── Property → Owner (restrict: can't delete owner who has properties) ──
            modelBuilder.Entity<Property>()
                .HasOne(p => p.Owner)
                .WithMany(u => u.Properties)
                .HasForeignKey(p => p.OwnerId)
                .OnDelete(DeleteBehavior.Restrict);

            // ── Booking → Property ──
            modelBuilder.Entity<Booking>()
                .HasOne(b => b.Property)
                .WithMany(p => p.Bookings)
                .HasForeignKey(b => b.PropertyId)
                .OnDelete(DeleteBehavior.Cascade);

            // ── Booking → Renter (NoAction avoids multiple cascade paths) ──
            modelBuilder.Entity<Booking>()
                .HasOne(b => b.Renter)
                .WithMany(u => u.Bookings)
                .HasForeignKey(b => b.RenterId)
                .OnDelete(DeleteBehavior.NoAction);

            // ── Notification → User ──
            modelBuilder.Entity<Notification>()
                .HasOne(n => n.User)
                .WithMany()
                .HasForeignKey(n => n.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            // ── SavedProperty — composite PK ──
            modelBuilder.Entity<SavedProperty>()
                .HasKey(s => new { s.UserId, s.PropertyId });

            modelBuilder.Entity<SavedProperty>()
                .HasOne(s => s.User)
                .WithMany()
                .HasForeignKey(s => s.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<SavedProperty>()
                .HasOne(s => s.Property)
                .WithMany()
                .HasForeignKey(s => s.PropertyId)
                .OnDelete(DeleteBehavior.Cascade);

            // ── Decimal precision ──
            modelBuilder.Entity<Property>()
                .Property(p => p.Rent)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<Property>()
                .Property(p => p.Deposit)
                .HasColumnType("decimal(18,2)");
        }
    }
}