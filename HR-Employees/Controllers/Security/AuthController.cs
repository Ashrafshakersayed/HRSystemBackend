using AutoMapper;
using HR_Employees.Dtos;
using HR_Employees.Entities;
using HR_Employees.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Scripting;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace HR_Employees.Controllers.Security
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly DBContext _context;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;

        const int keySize = 64;
        const int iterations = 350000;
        HashAlgorithmName hashAlgorithm = HashAlgorithmName.SHA512;

        public AuthController(DBContext context, IMapper mapper, IConfiguration configuration)
        {
            _context = context;
            _mapper = mapper;
            _configuration = configuration;
        }


        // POST: api/Auth/Register
        [HttpPost("Register")]
        public async Task<ActionResult<User>> Register(UserDto model)
        {
            // validate
            if (_context.Users.Any(x => x.Email == model.Email))
                throw new AppException("Email '" + model.Email + "' is already taken");

            // map model to new user object
            var user = _mapper.Map<User>(model);

            // hash password
            user.PasswordHash = HashPassword(model.Password, out byte[] salt);
            user.PasswordSalt = salt;

            //save user
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return CreatedAtAction("Login", new { id = user.Id }, user);
        }


        // GET: api/Auth/Login
        [HttpPost("Login")]
        public async Task<ActionResult<AuthenticateResponse>> Login(LoginDto model)
        {
            var user = _context.Users.SingleOrDefault(x => x.Email == model.Email);

            // validate
            if (user == null || !VerifyPassword(model.Password, user.PasswordHash, user.PasswordSalt))
                throw new AppException("Username or password is incorrect");

            // authentication successful
            var response = _mapper.Map<AuthenticateResponse>(user);
            response.Token = GenerateToken(user);
            return response;

        }

        private string GenerateToken(User user)
        {
            var secretKey = new SymmetricSecurityKey
            (Encoding.UTF8.GetBytes(_configuration.GetValue<string>("Jwt:Key")));
            var signinCredentials = new SigningCredentials
           (secretKey, SecurityAlgorithms.HmacSha256);
            var jwtSecurityToken = new JwtSecurityToken(
                issuer: _configuration.GetValue<string>("Jwt:Issuer"),
                audience: _configuration.GetValue<string>("Jwt:Audience"),
                claims: new List<Claim>(new[] { new Claim("id", user.Id.ToString()) }),
                expires: DateTime.Now.AddDays(7),
                signingCredentials: signinCredentials
            );
            return new JwtSecurityTokenHandler().
            WriteToken(jwtSecurityToken);
        }

        private byte[] HashPassword(string password, out byte[] salt)
        {
            salt = RandomNumberGenerator.GetBytes(keySize);
            var hash = Rfc2898DeriveBytes.Pbkdf2(
                Encoding.UTF8.GetBytes(password),
                salt,
                iterations,
                hashAlgorithm,
                keySize);
            return hash;
        }

        private bool VerifyPassword(string password, byte[] hash, byte[] salt)
        {
            var hashToCompare = Rfc2898DeriveBytes.Pbkdf2(password, salt, iterations, hashAlgorithm, keySize);

            return CryptographicOperations.FixedTimeEquals(hashToCompare, hash);
        }
    }
}
