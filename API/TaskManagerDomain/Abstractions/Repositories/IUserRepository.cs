using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagerDomain.Models;

namespace TaskManagerDomain.Abstractions.Repositories
{
    public interface IUserRepository
    {
        Task<User> RegisterUser(User user, string password);
        Task<bool> LoginUser(User user, string password);
        Task<RefreshToken> SetToken(string username, RefreshToken newRefreshToken);
        Task<User> GetUserByName(string username);

    }
}
