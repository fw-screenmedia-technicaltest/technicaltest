using System;
using Microsoft.EntityFrameworkCore;
using ScreenMediaTT.Data.Models;

namespace ScreenMediaTT.Data
{
    public class ScreenMediaTtContext : DbContext
    {
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<Hotel> Hotels { get; set; }
        public DbSet<PersonalDetails> PersonalDetails { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<RoomAvailability> RoomAvailability { get; set; }
        public DbSet<RoomType> RoomTypes { get; set; }

        public DbSet<RoomBooking> RoomBookings { get; set; }

        public string DbPath { get; private set; }

        public ScreenMediaTtContext(DbContextOptions options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Configure domain classes using modelBuilder here   
            modelBuilder.Entity<RoomAvailability>()
              .HasKey(e => new { e.Date, e.RoomID });
        }
    }
}
