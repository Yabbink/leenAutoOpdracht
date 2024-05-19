namespace GraafschapCollegeApi.Entities
{
    using Microsoft.EntityFrameworkCore;

    using System.ComponentModel.DataAnnotations;

    [Index(nameof(Email), IsUnique = true)]
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public virtual ICollection<Role> Roles { get; set; }
    }
}
