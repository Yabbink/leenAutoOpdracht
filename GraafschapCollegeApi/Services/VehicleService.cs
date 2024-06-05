using GraafschapCollegeApi.Context;
using Microsoft.EntityFrameworkCore;
using GraafschapCollege.Shared.Responses;

namespace GraafschapCollegeApi.Services
{
    public class VehicleService(GraafschapCollegeDbContext dbContext)
    {
        public IEnumerable<VehicleResponse> GetVehicles()
        {
            return dbContext.Vehicles.Select(x => new VehicleResponse
            {
                Id = x.Id,
                Brand = x.Brand,
                Model = x.Model
            }).ToList();
        }
    }
}
