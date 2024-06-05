using GraafschapCollegeApi.Context;
using GraafschapCollegeApi.Entities;
using GraafschapCollege.Shared.Constants;
using System.Data;

namespace GraafschapCollegeApi.Seeders
{
    public static class UserSeeder
    {
        public static void Seed(GraafschapCollegeDbContext dbContext)
        {
            var existingUsers = dbContext.Users
                .Select(x => x.Email)
                .ToList();

            var roles = dbContext.Roles.ToList();

            var users = new List<User>
            {
                new()
                {
                    Name = "Bryan Schoot",
                    Email = "b.schoot@example.com",
                    Password = BCrypt.Net.BCrypt.HashPassword("Password123!"),
                    Roles = [roles.Find(x => x.Name == Roles.Administrator)!]
                },
                new()
                {  
                    Name = "John Doe",
                    Email = "j.doe@example.com",
                    Password = BCrypt.Net.BCrypt.HashPassword("Password123!"),
                    Roles = [roles.Find(x => x.Name == Roles.Employee)!]
                }
            };

            var usersToAdd = users
                .Where(x => !existingUsers.Contains(x.Email))
                .ToList();

            dbContext.Users.AddRange(usersToAdd);
            dbContext.SaveChanges();
        }
    }
}