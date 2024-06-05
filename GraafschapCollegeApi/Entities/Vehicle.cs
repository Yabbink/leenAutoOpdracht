using System.ComponentModel.DataAnnotations;

namespace GraafschapCollegeApi.Entities
{
    public class Vehicle
    {
        [Key]
        public int Id { get; set; }
        public string LicensePlate { get; set; }
        public int Odometer { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public DateOnly ManufacturedDate { get; set; }
        public virtual ICollection<Reservation> Reservations { get; set; }
    }
}
