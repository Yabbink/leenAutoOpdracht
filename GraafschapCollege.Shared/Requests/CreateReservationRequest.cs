using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraafschapCollege.Shared.Requests
{
    public class CreateReservationRequest
    {
        public int VehicleId { get; set; }
        public DateTime From { get; set; }
        public DateTime Until { get; set; }
    }
}
