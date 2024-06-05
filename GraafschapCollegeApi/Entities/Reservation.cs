using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace GraafschapCollegeApi.Entities
{
    public class Reservation
    {
        [Key]
        public int Id { get; set; }

        public int UserId { get; set; }
        [ForeignKey(nameof(UserId))]
        public User User { get; set; }

        public int VehicleId { get; set; }
        [ForeignKey(nameof(VehicleId))]
        public Vehicle Vehicle { get; set; }

        public DateTime From { get; set; }
        public DateTime Until { get; set; }

        public int? TripId { get; set; }
        [ForeignKey(nameof(TripId))]
        public Trip? Trip { get; set; }
    }
}
