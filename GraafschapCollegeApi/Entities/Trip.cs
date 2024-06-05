using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace GraafschapCollegeApi.Entities
{
    public class Trip
    {
        [Key]
        public int Id { get; set; }
        public int ReservationId { get; set; }
        [ForeignKey(nameof(ReservationId))]
        public Reservation Reservation { get; set; }
        public int OdometerStart { get; set; }
        public int OdometerEnd { get; set; }
        public virtual ICollection<Address>? Addresses { get; set; }
    }
}
