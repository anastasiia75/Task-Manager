using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Security.Cryptography;
using TaskManagerAPI.Dtos;
using TaskManagerDAL.Repositories;
using System.IdentityModel.Tokens.Jwt;
using TaskManagerDomain.Abstractions.Repositories;
using TaskManagerDomain.Abstractions.Services;
using TaskManagerDomain.Models;
using Microsoft.AspNetCore.Authorization;

namespace TaskManagerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly IConfiguration _config;
        private readonly IUserServices _userServices;

        public AuthController(IUserRepository userRepository, IMapper mapper, IConfiguration configuration, IUserServices userServices)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _config = configuration;
            _userServices = userServices;
        }

        [HttpGet,Authorize]
        public async Task<ActionResult<string>> GetUsername()
        {
            var username = await _userServices.GetUsername();
            return Ok(username);
        }

        [HttpPost("register(regPopup:register)")]
        public async Task<ActionResult<string>> Register(UserDto request)
        {
            var user = _mapper.Map<User>(request);
            var registeredUser = await _userRepository.RegisterUser(user, request.Password);
            if (registeredUser == null)
                return BadRequest("User with this username already exists.");
            
            string token = CreateToken(user);

            var newRefreshToken = GenerateRefreshToken();

            await _userRepository.SetToken(request.Username, SetRefreshToken(newRefreshToken));

            return token;

        }

        [HttpPost("login(loginPopup:login)")]
        public async Task<ActionResult<string>> Login(UserDto request)
        {
            var user = _mapper.Map<User>(request);
            if (!await _userRepository.LoginUser(user, request.Password))
                return BadRequest("Incorrect input data");

            string token = CreateToken(user);

            var newRefreshToken = GenerateRefreshToken();

            await _userRepository.SetToken(request.Username, SetRefreshToken(newRefreshToken));

            return token;
        }

        private RefreshToken SetRefreshToken(RefreshToken newRefreshToken)
        {
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Expires = newRefreshToken.Expires
            };
            Response.Cookies.Append("refreshToken", newRefreshToken.Token, cookieOptions);

            return newRefreshToken;
        }

        private RefreshToken GenerateRefreshToken()
        {
            var refreshToken = new RefreshToken
            {
                Token = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64)),
                Expires = DateTime.Now.AddDays(7),
                Created = DateTime.Now
            };

            return refreshToken;
        }

        private string CreateToken(User user)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Username)
            };

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(
                _config.GetSection("AppSettings:Token").Value));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: creds);

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }



    }
}
