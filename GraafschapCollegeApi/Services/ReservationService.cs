using GraafschapCollege.Shared.Constants;
using GraafschapCollege.Shared.Interfaces;
using GraafschapCollege.Shared.Requests;
using GraafschapCollege.Shared.Responses;
using GraafschapCollegeApi.Context;
using GraafschapCollegeApi.Entities;
using Microsoft.EntityFrameworkCore;

namespace GraafschapCollegeApi.Services
{
    public class ReservationService(GraafschapCollegeDbContext dbContext, ICurrentUserContext currentUserContext)
    {
        public ReservationResponse CreateReservation(CreateReservationRequest request)
        {
            var response = new ReservationResponse();

            var user = dbContext.Users.Find(currentUserContext.User.Id);
            if (user == null)
            {
                response.Errors.Add("User not found");
            }

            var vehicle = dbContext.Vehicles.Find(request.VehicleId);
            if (vehicle == null)
            {
                response.Errors.Add("Vehicle not found");
            }

            var isReserved = dbContext.Reservations.Any(x => x.VehicleId == request.VehicleId
                && !(x.Until <= request.From || x.From >= request.Until));
            if (isReserved)
            {
                response.Errors.Add("Vehicle is already reserved");
            }

            var isDateValid = request.From > request.Until;
            if (isDateValid)
            {
                response.Errors.Add("Dates not valid");
            }

            if (response.Errors.Count > 0)
            {
                return response;
            }

            var reservation = new Reservation
            {
                UserId = user!.Id,
                VehicleId = vehicle!.Id,
                From = request.From,
                Until = request.Until
            };

            dbContext.Reservations.Add(reservation);
            dbContext.SaveChanges();

            return new ReservationResponse
            {
                Id = reservation.Id,
                From = reservation.From,
                Until = reservation.Until,
                User = new ReservationResponse.UserResponse(user.Id, user.Name),
                Vehicle = new ReservationResponse.VehicleResponse(vehicle.Id, vehicle.Brand, vehicle.Model, vehicle.ManufacturedDate)
            };
        }

        public List<ReservationResponse> GetReservations()
        {
            var queryable = dbContext.Reservations.AsQueryable();

            if (currentUserContext.IsInRole(Roles.Employee))
            {
                queryable = queryable.Where(x => x.UserId == currentUserContext.User.Id);
            }

            var reservations = queryable
                .Select(x => new ReservationResponse
                {
                    Id = x.Id,
                    From = x.From,
                    Until = x.Until,
                    User = new ReservationResponse.UserResponse(x.User.Id, x.User.Name),
                    Vehicle = new ReservationResponse.VehicleResponse(x.Vehicle.Id, x.Vehicle.Brand, x.Vehicle.Model, x.Vehicle.ManufacturedDate)
                })
                .ToList();
            return reservations;
        }

        public ReservationResponse? GetReservationById(int id)
        {
            var queryable = dbContext.Reservations.AsQueryable();

            if (currentUserContext.IsInRole(Roles.Employee))
            {
                queryable = queryable.Where(x => x.UserId == currentUserContext.User.Id);
            }

            var reservation = queryable.FirstOrDefault(x => x.Id == id);

            if (reservation == null)
            {
                return null;
            }

            return new ReservationResponse
            {
                Id = reservation.Id,
                From = reservation.From,
                Until = reservation.Until,
                User = new ReservationResponse.UserResponse(reservation.User.Id, reservation.User.Name),
                Vehicle = new ReservationResponse.VehicleResponse(reservation.Vehicle.Id, reservation.Vehicle.Brand, reservation.Vehicle.Model, reservation.Vehicle.ManufacturedDate)
            };
        }

        public void DeleteReservation(int id)
        {
            var queryable = dbContext.Reservations.AsQueryable();

            if (!currentUserContext.IsInRole(Roles.Employee))
            {
                queryable = queryable.Where(x => x.UserId == currentUserContext.User.Id);
            }

            var reservation = queryable
                .Where(x => x.Until >= DateTime.Now)
            .Where(x => x.TripId == null)
                .FirstOrDefault(x => x.Id == id);

            if (reservation == null)
            {
                return;
            }

            dbContext.Reservations.Remove(reservation);
            dbContext.SaveChanges();
        }
    }
}
