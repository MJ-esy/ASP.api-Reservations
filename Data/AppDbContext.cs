using ASP_Reservations.Models;
using Microsoft.EntityFrameworkCore;

namespace ASP_Reservations.Data
{
  public class AppDbContext : DbContext
  {
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    { }

    public DbSet<User> Users { get; set; }
    public DbSet<Table> Tables { get; set; }
    public DbSet<Booking> Booking { get; set; }
    public DbSet<Dish> Dishes { get; set; }
    public DbSet<Admin> Admins { get; set; }


    //Fluent API configurations
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      base.OnModelCreating(modelBuilder);

      //User
      var userEntity = modelBuilder.Entity<User>();
      userEntity.Property(u => u.Name).IsRequired();
      userEntity.HasIndex(u => new { u.Email, u.Phone }).IsUnique();

      //Table
      var tableEntity = modelBuilder.Entity<Table>();
      tableEntity.HasIndex(t => t.Capacity);

      //Booking
      var bookingEntity = modelBuilder.Entity<Booking>();
      bookingEntity.Property(b => b.GuestNum).HasDefaultValue(1)
          .IsRequired();
      bookingEntity.HasIndex(b => new { b.Status, b.TableIdFk, b.StartDateTime })
          .HasFilter("[Status] != 'Cancelled'");
      bookingEntity.Property(e => e.Status)
             .IsRequired()
             .HasConversion<string>()
             .HasMaxLength(20);
      bookingEntity.Property(e => e.CreatedAt)
              .IsRequired()
              .HasDefaultValueSql("GETUTCDATE()");
      bookingEntity.HasOne(e => e.User)
             .WithMany(c => c.Bookings)
             .HasForeignKey(e => e.UserIdFk)
             .OnDelete(DeleteBehavior.Restrict)
             .HasConstraintName("FK_Bookings_Customers");
      bookingEntity.HasOne(e => e.Table)
               .WithMany(c => c.Bookings)
               .HasForeignKey(e => e.TableIdFk)
               .OnDelete(DeleteBehavior.Restrict)
               .HasConstraintName("FK_Bookings_Tables");

      //Dish
      var dishEntity = modelBuilder.Entity<Dish>();
      dishEntity.Property(d => d.DishName).IsRequired();
      dishEntity.Property(d => d.IsPopular).HasDefaultValue(false);
      dishEntity.HasIndex(d => d.IsPopular);
      dishEntity.Property(d => d.Allergen).IsRequired();

      //Admin
      var adminEntity = modelBuilder.Entity<Admin>();
      adminEntity.HasIndex(a => a.Username).IsUnique();
      adminEntity.Property(a => a.Username).IsRequired();
      adminEntity.Property(a => a.PasswordHash).IsRequired()
          .HasMaxLength(100);
      adminEntity.Property(a => a.Role).IsRequired()
          .HasMaxLength(15)
          .HasDefaultValue("ADMIN");

      //Data Seeding
      modelBuilder.SeedAdmin();
      modelBuilder.SeedDishes();
      modelBuilder.SeedUsers();
      modelBuilder.SeedTables();
    }


  }
}
