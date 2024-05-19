using GraafschapCollegeApi.Context;
using GraafschapCollegeApi.Entities;
using GraafschapCollege.Shared.Constants;
using System.Data;

namespace GraafschapCollegeApi.Seeders
{
    public class RoleSeeder
    {
        public static void Seed(GraafschapCollegeDbContext dbContext)
        {
            var existingRoles = dbContext.Roles
            .Select(x => x.Name)
            .ToList();

            var roles = new List<Role>
            {
            new() { Name = Roles.Administrator },
            new() { Name = Roles.Employee }
            };

            var rolesToAdd = roles
                .Where(x => !existingRoles.Contains(x.Name))
                .ToList();

            dbContext.Roles.AddRange(rolesToAdd);
            dbContext.SaveChanges();
        }
    }
}
