using HMS.Models.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace HMS.Repository.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<Hotel> Hotels { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Manager> Managers { get; set; }
        public DbSet<Guest> Guests { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder); // Ensure Identity configuration is applied

            //modelBuilder.Entity<ApplicationUser>(entity =>
            //{
            //    // Ensure unique IdentityNumber & PhoneNumber (already defined in attributes)
            //    entity.HasIndex(e => e.IdentityNumber).IsUnique();
            //    entity.HasIndex(e => e.PhoneNumber).IsUnique();

            //    // Role column should not be null
            //    entity.Property(e => e.Role)
            //        .IsRequired();

            //    // Guest: One user can have many bookings
            //    entity.HasMany(e => e.Bookings)
            //        .WithOne(b => b.Guest)
            //        .HasForeignKey(b => b.UserId)
            //        .OnDelete(DeleteBehavior.Cascade); // Deleting a guest should delete bookings

            //    // Manager: One user can manage one hotel
            //    entity.HasOne(e => e.Hotel)
            //        .WithMany(h => h.Managers)
            //        .HasForeignKey(e => e.HotelId)
            //        .OnDelete(DeleteBehavior.Restrict); // Prevent accidental deletion


            //    // Ensure only one relationship is used (Hotel OR Bookings, not both)

            //});
            modelBuilder.ChangeDefaultTableNames();
            modelBuilder.ConfigureApplicationUser();
            modelBuilder.ConfigureBookings();
            modelBuilder.ConfigureHotels();
            modelBuilder.ConfigureRooms();
            modelBuilder.ConfigureGuest();
            modelBuilder.ConfigureManager();
        }
    }
}
