namespace GraafschapCollegeApi.Seeders
{
    using GraafschapCollegeApi.Context;
    public static class Seeder
    {
        public static void Seed(this GraafschapCollegeDbContext dbContext)
        {
            UserSeeder.Seed(dbContext);
            RoleSeeder.Seed(dbContext);
        }
    }
}
