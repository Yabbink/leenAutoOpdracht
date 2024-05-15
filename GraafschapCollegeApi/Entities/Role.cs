namespace GraafschapCollegeApi.Entities
{
    using System.ComponentModel.DataAnnotations;

    public class Role
    {
        public const string Administrator = "Administrator";
        public const string Employee = "Employee";

        public int Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<User> Users { get; set; }
    }
}
