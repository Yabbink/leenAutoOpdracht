using GraafschapCollegeApi.Entities;
using Microsoft.EntityFrameworkCore;

namespace GraafschapCollegeApi.Context
{
    public class GraafschapCollegeDbContext(DbContextOptions<GraafschapCollegeDbContext> options)
        : DbContext(options)
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
    }
}
