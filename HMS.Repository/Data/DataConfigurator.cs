using HMS.Models.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace HMS.Repository.Data
{
    public static class DataConfigurator
    {
        public static void ChangeDefaultTableNames(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ApplicationUser>(entity => entity.ToTable(name: "Users"));
            modelBuilder.Entity<IdentityRole>(entity => entity.ToTable(name: "Roles"));
            modelBuilder.Entity<IdentityUserRole<string>>(entity => entity.ToTable(name: "UserRoles"));
            modelBuilder.Entity<IdentityUserClaim<string>>(entity => entity.ToTable(name: "UserClaims"));
            modelBuilder.Entity<IdentityUserLogin<string>>(entity => entity.ToTable(name: "UserLogins"));
            modelBuilder.Entity<IdentityRoleClaim<string>>(entity => entity.ToTable(name: "RoleClaims"));
            modelBuilder.Entity<IdentityUserToken<string>>(entity => entity.ToTable(name: "UserTokens"));
        }

        public static void ConfigureHotels(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Hotel>(entity =>
            {
                entity.HasKey(h => h.Id);
                entity
                .Property(h => h.Id)
                 .ValueGeneratedOnAdd()
                .IsRequired();

                entity
                .Property(h => h.Name)
                .IsRequired()
                .HasMaxLength(50);
                entity
                .Property(h => h.Rating)
                .IsRequired()
                .HasColumnType("decimal(2,1)");
                entity
                .Property(h => h.Country)
                .IsRequired()
                .HasMaxLength(50);
                entity
                .Property(h => h.City)
                .IsRequired()
                .HasMaxLength(50);
                entity
                .Property(h => h.Address)
                .IsRequired()
                .HasMaxLength(70);
                entity
                .HasMany(h => h.Rooms)
                .WithOne(r => r.Hotel)
                .HasForeignKey(r => r.HotelId)
                .OnDelete(DeleteBehavior.Cascade);
            });
            }
        public static void ConfigureRooms(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Room>(entity =>
            {
                entity.HasKey(r => r.Id);
                entity
                .Property(r => r.Id)
                .ValueGeneratedOnAdd()
                .IsRequired();
                entity
                .Property(r => r.RoomType)
                .IsRequired()
                .HasMaxLength(50);
                entity
                .Property(r => r.IsAvailable)
                .IsRequired()
                .HasColumnType("bit");
                entity
                .Property(r => r.Price)
                .IsRequired()
                .HasColumnType("decimal(10,2)");
                entity
                .HasOne(r => r.Hotel)
                .WithMany(h => h.Rooms)
                .HasForeignKey(r => r.HotelId)
                .OnDelete(DeleteBehavior.Restrict);
                entity
                .HasMany(r => r.Bookings)
                .WithOne(b => b.Room)
                .HasForeignKey(b => b.RoomId)
                .OnDelete(DeleteBehavior.Cascade);
                ;
            });
        }
        public static void ConfigureBookings(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Booking>(entity =>
            {
                entity.HasKey(b => b.Id);
                entity
                .Property(b => b.Id)
                .ValueGeneratedOnAdd()
                .IsRequired();
                entity
                .Property(b => b.CheckInDate)
                .IsRequired()
                .HasColumnType("datetime2");
                entity
                .Property(b => b.CheckOutDate)
                .IsRequired()
                .HasColumnType("datetime2");
                entity
                .HasOne(b => b.Guest)
                .WithMany(g => g.Bookings)
                .HasForeignKey(b => b.GuestId)
                .OnDelete(DeleteBehavior.Cascade);
                entity
                .HasOne(b => b.Room)
                .WithMany(r => r.Bookings)
                .HasForeignKey(b => b.RoomId)
                .OnDelete(DeleteBehavior.Restrict);

            });
        }
        public static void ConfigureApplicationUser(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ApplicationUser>(entity =>
            {
                // unique IdentityNumber & PhoneNumber
                entity.HasIndex(e => e.IdentityNumber).IsUnique();
                entity.HasIndex(e => e.PhoneNumber).IsUnique();
                entity
                    .Property(u => u.Email)
                    .IsRequired(false);

                // Navigation property to Manager
                entity.HasOne(u => u.Manager)
                      .WithOne(m => m.ApplicationUser)
                      .HasForeignKey<Manager>(m => m.UserId)
                      .OnDelete(DeleteBehavior.Restrict);

                // Navigation property to Guest
                entity.HasOne(u => u.Guest)
                      .WithOne(g => g.User)
                      .HasForeignKey<Guest>(g => g.UserId)
                      .OnDelete(DeleteBehavior.Restrict);
            });
        }
        public static void ConfigureGuest(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Guest>(entity =>
            {
                entity.HasKey(g => g.UserId);
                entity.HasMany(g => g.Bookings)
                    .WithOne(b => b.Guest)
                    .HasForeignKey(b => b.GuestId)
                    .OnDelete(DeleteBehavior.Cascade);
            });
        }
        public static void ConfigureManager(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Manager>(entity =>
            {
                entity.HasKey(m => m.UserId);

                // Foreign key to ApplicationUser
                entity.HasOne(m => m.ApplicationUser)
                      .WithOne(u => u.Manager)
                      .HasForeignKey<Manager>(m => m.UserId)
                      .OnDelete(DeleteBehavior.Restrict);

                // Foreign key to Hotel
                entity.HasOne(m => m.Hotel)
                      .WithMany(h => h.Managers)
                      .HasForeignKey(m => m.HotelId)
                      .OnDelete(DeleteBehavior.Restrict);
            });
        }
     }
}
