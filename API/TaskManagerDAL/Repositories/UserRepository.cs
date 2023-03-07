using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

using TaskManagerDomain.Abstractions.Repositories;
using TaskManagerDomain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;

namespace TaskManagerDAL.Repositories
{
    public class UserRepository: IUserRepository
    {
        private readonly DataContext _dataContext;
        public UserRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<User> RegisterUser(User user, string password)
        {
            var userToCheck = _dataContext.Users.FirstOrDefault(x => x.Username == user.Username);
            if (userToCheck != null)
                return null;

            CreatePasswordHash(password, out byte[] passwordHash, out byte[] passwordSalt);
            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;

            _dataContext.Users.Add(user);
            await _dataContext.SaveChangesAsync();
            return user;
        }

        public async Task<bool> LoginUser(User user,string password)
        { 
            var userExist = await _dataContext.Users.FirstOrDefaultAsync(x => x.Username == user.Username);
            if (userExist == null || !VerifyPasswordHash(password, userExist.PasswordHash,userExist.PasswordSalt))
            {
                return false;
            }

            return true;
            
        }

        public async Task<RefreshToken> SetToken(string username, RefreshToken newRefreshToken)
        {
            var user = await _dataContext.Users.FirstOrDefaultAsync(x => x.Username == username);

            user.RefreshToken = newRefreshToken.Token;
            user.TokenCreated = newRefreshToken.Created;
            user.TokenExpires = newRefreshToken.Expires;
            return newRefreshToken;
        }

        public async Task<User> GetUserByName(string username)
        {
            var user =await  _dataContext.Users.FirstOrDefaultAsync(x => x.Username == username);
            return user;
        }

        public void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }
        public bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return computedHash.SequenceEqual(passwordHash);
            }

        }

    }
}
