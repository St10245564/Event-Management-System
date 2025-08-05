using Microsoft.EntityFrameworkCore;
using MyPart3.Models;

namespace MyPart3.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<EventType> EventTypes { get; set; }
        public DbSet<Venue> Venues { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<Booking> Bookings { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // ================================
            // EventType
            // ================================
            modelBuilder.Entity<EventType>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name)
                      .IsRequired()
                      .HasMaxLength(100);
            });

            // ================================
            // Venue
            // ================================
            modelBuilder.Entity<Venue>(entity =>
            {
                entity.HasKey(v => v.Id);

                entity.Property(v => v.Name)
                      .IsRequired()
                      .HasMaxLength(100);

                entity.Property(v => v.Location)
                      .IsRequired()
                      .HasMaxLength(255);

                entity.Property(v => v.Capacity)
                      .IsRequired();

                entity.Property(v => v.IsAvailable)
                      .HasDefaultValue(true);

                entity.Property(v => v.ImageUrl)
                      .HasMaxLength(500);

                entity.HasOne(v => v.EventType)
                      .WithMany(et => et.Venues)
                      .HasForeignKey(v => v.EventTypeId)
                      .OnDelete(DeleteBehavior.Restrict)
                      .HasConstraintName("FK_Venue_EventType");
            });

            // ================================
            // Event
            // ================================
            modelBuilder.Entity<Event>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.Property(e => e.Name)
                      .IsRequired()
                      .HasMaxLength(150);

                entity.Property(e => e.Date)
                      .IsRequired();

                entity.HasOne(e => e.Venue)
                      .WithMany(v => v.Events)
                      .HasForeignKey(e => e.VenueId)
                      .OnDelete(DeleteBehavior.Restrict)
                      .HasConstraintName("FK_Event_Venue");

                entity.HasOne(e => e.EventType)
                      .WithMany(et => et.Events)
                      .HasForeignKey(e => e.EventTypeId)
                      .OnDelete(DeleteBehavior.Restrict)
                      .HasConstraintName("FK_Event_EventType");
            });

            // ================================
            // Booking
            // ================================
            modelBuilder.Entity<Booking>(entity =>
            {
                entity.HasKey(b => b.Id);

                entity.Property(b => b.BookingDate)
                      .IsRequired()
                      .HasColumnType("datetime");

                entity.HasOne(b => b.Event)
                      .WithMany(e => e.Bookings)
                      .HasForeignKey(b => b.EventId)
                      .OnDelete(DeleteBehavior.Cascade)
                      .HasConstraintName("FK_Booking_Event");

                entity.HasOne(b => b.Venue)
                      .WithMany()
                      .HasForeignKey(b => b.VenueId)
                      .OnDelete(DeleteBehavior.Restrict)
                      .HasConstraintName("FK_Booking_Venue");
            });
        }
    }
}
