using GraafschapCollege.Shared.Requests;
using GraafschapCollege.Shared.Responses;
using GraafschapCollegeApi.Context;
using GraafschapCollegeApi.Entities;

namespace GraafschapCollegeApi.Services
{
    public class UserService(GraafschapCollegeDbContext dbContext)
    {
        public IEnumerable<UserResponse> GetUsers()
        {
            return dbContext.Users.Select(x => new UserResponse
            {
                Id = x.Id,
                Name = x.Name,
                Email = x.Email
            });
        }

        public UserResponse? GetUserById(int id)
        {
            var user = dbContext.Users.Find(id);

            if (user == null)
            {
                return null;
            }

            return new UserResponse
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email
            };
        }

        public UserResponse CreateUser(CreateUserRequest request)
        {
            var existingUser = dbContext.Users
                .SingleOrDefault(x => x.Email == request.Email);

            if (existingUser != null)
            {
                throw new Exception("User already exists");
            }

            var roles = dbContext.Roles
                .Where(x => request.Roles.Contains(x.Id))
                .ToList();

            var user = new User
            {
                Name = request.Name,
                Email = request.Email,
                Password = BCrypt.Net.BCrypt.HashPassword(request.Password),
                Roles = roles
            };

            dbContext.Users.Add(user);
            dbContext.SaveChanges();

            return new UserResponse
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email
            };
        }

        public UserResponse? UpdateUser(int id, UpdateUserRequest request)
        {
            var user = dbContext.Users.Find(id);

            if (user == null)
            {
                return null;
            }

            user.Name = request.Name;
            user.Email = request.Email;

            dbContext.SaveChanges();

            return new UserResponse
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email
            };
        }

        public UserResponse? DeleteUser(int id)
        {
            var user = dbContext.Users.Find(id);

            if (user == null)
            {
                return null;
            }

            dbContext.Users.Remove(user);
            dbContext.SaveChanges();

            return new UserResponse
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email
            };
        }
    }
}
