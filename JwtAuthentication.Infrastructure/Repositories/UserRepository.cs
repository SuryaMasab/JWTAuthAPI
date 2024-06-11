using JwtAuthentication.Domain;
using JwtAuthentication.Infrastructure.Repositories.Interface;

namespace JwtAuthentication.Infrastructure.Repositories;

public class UserRepository :IUserRepository
{

    #region GetUser
    public User? GetUser(string userId)
    {
        // fill static data 
        var user = FillUsers().FirstOrDefault(x => x.Email == userId);

        if (user == null)
        {
            return null;
        }

        return user;
    }

    #endregion    

    #region GetUsers
    public List<User> GetUsers()
    {
        List<User> users = FillUsers();

        return users;
    }

    #endregion

    #region Private Methods
    private static List<User> FillUsers()
    {
        // fill some static set of Users
        return
        [
            new() {
                Name = "UserOne",
                Email = "userone@test.com",
                Password= "123456",
                Latitude = 12.9716,
                Longitude = 77.5946,
            },
            new() {
                Name = "UserTwo",
                Email = "usertwo@test.com",
                Password= "123456",
                Latitude = 11.9716,
                Longitude = 67.5946,
            }
        ];
    }

    #endregion
}
