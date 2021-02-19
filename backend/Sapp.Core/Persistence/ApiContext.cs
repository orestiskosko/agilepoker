using Microsoft.EntityFrameworkCore;
using Sapp.Core.Entities;

namespace Sapp.Core.Persistence
{
    public class ApiContext : DbContext
    {
        public ApiContext(DbContextOptions<ApiContext> options) : base(options)
        { }

        public DbSet<User> Users { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<RoomUser> RoomUsers { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<Vote> Votes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<RoomUser>()
                .HasKey(
                    e =>
                        new
                        {
                            e.RoomId,
                            e.UserId,
                        });
            
            modelBuilder
                .Entity<Vote>()
                .HasKey(
                    e =>
                        new
                        {
                            e.RoomId,
                            e.UserId,
                            e.ItemId
                        });
        }
    }
}