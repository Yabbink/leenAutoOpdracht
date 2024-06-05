using GraafschapCollegeApi.Entities;
using Microsoft.EntityFrameworkCore;

namespace GraafschapCollegeApi.Context
{
    public class GraafschapCollegeDbContext(DbContextOptions<GraafschapCollegeDbContext> options)
        : DbContext(options)
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<Trip> Trips { get; set; }
    }
}
