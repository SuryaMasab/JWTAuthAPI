using JwtAuthentication.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JwtAuthentication.Infrastructure.Repositories.Interface;

public interface IUserRepository
{
    public List<User> GetUsers();

    public User? GetUser(string userId);
}
