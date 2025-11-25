using MeetingRoomTrackerLib.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace MeetingRoomTrackerLib
{
    public class RMTDbContext : DbContext
    {
        public RMTDbContext(DbContextOptions<RMTDbContext> options) : base(options) 
        { 
        
        }

        public DbSet<Room> Rooms { get; set; }
        public DbSet<TimeLog> TimeLogs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Room configuration
            modelBuilder.Entity<Room>(entity =>
            {
                entity.HasKey(r => r.Id);
                entity.Property(r => r.Name).IsRequired().HasMaxLength(100);
                entity.Property(r => r.RoomType).HasConversion<int>(); // Store enum as int
                entity.Property(r => r.Building).HasConversion<int>();

                // Optional: unique constraint on Name + Building + Floor
                entity.HasIndex(r => new { r.Name, r.Building, r.Floor }).IsUnique();
            });

            // TimeLog configuration
            modelBuilder.Entity<TimeLog>(entity =>
            {
                // Primary key
                entity.HasKey(t => t.Id);
                // Properties
                entity.Property(t => t.StartEvent).IsRequired();
                entity.Property(t => t.EndEvent).IsRequired();
                entity.Property(t2 => t2.IntervalTimer).HasDefaultValueSql("GETUTCDATE()");

                // Foreign key
                entity.HasOne<Room>(t => t.Room).WithMany().HasForeignKey(t => t.RoomId).OnDelete(DeleteBehavior.Cascade);
                
            });

            base.OnModelCreating(modelBuilder);
        }



    }
}
