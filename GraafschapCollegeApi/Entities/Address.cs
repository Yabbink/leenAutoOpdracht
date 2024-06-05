using System.ComponentModel.DataAnnotations;

namespace GraafschapCollegeApi.Entities
{
    public class Address
    {
        [Key]
        public int Id { get; set; }
        public string Street { get; set; }
        public string Number { get; set; }
        public string City { get; set; }
        public string PostalCode { get; set; }
        public string Country { get; set; }
        public long? Longitude { get; set; }
        public long? Latitude { get; set; }
    }
}
