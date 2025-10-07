using GraphQL.Models;
using Microsoft.EntityFrameworkCore;

namespace WatchTogether.Backend.GraphQL.Data
{
  public sealed class ApplicationDbContext : DbContext
  {
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options) { }

    public DbSet<User> Users { get; init; } = null!;
    public DbSet<Room> Rooms { get; init; } = null!;
    public DbSet<Message> Messages { get; init; } = null!;
        public DbSet<Guest> Guests { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      base.OnModelCreating(modelBuilder);

      // Unique usernames
      modelBuilder.Entity<User>()
          .HasIndex(u => u.Username)
          .IsUnique();

      // Room → Owner (1:N)
      modelBuilder.Entity<Room>()
          .HasOne(r => r.Owner)
          .WithMany(u => u.OwnedRooms)
          .HasForeignKey(r => r.OwnerId)
          .OnDelete(DeleteBehavior.Cascade);

      // Message → User & Room
      modelBuilder.Entity<Message>()
          .HasOne(m => m.User)
          .WithMany(u => u.Messages)
          .HasForeignKey(m => m.UserId)
          .OnDelete(DeleteBehavior.Cascade);

      modelBuilder.Entity<Message>()
          .HasOne(m => m.Room)
          .WithMany(r => r.Messages)
          .HasForeignKey(m => m.RoomId)
          .OnDelete(DeleteBehavior.Cascade);
    }
  }
}
