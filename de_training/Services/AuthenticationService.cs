using de_training.Exceptions;
using de_training.Services.Abstractions;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace de_training.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly libraryContext _libraryContext;
        private readonly IConfiguration _configuration;

        public AuthenticationService(libraryContext libraryContext, IConfiguration configuration)
        {
            _libraryContext = libraryContext;
            _configuration = configuration;
        }

        public async Task<string> AuthenticateUserAsync(string name, string password)
        {
            var findedUser = await _libraryContext.Users.FirstOrDefaultAsync(x => x.Name.Equals(name));
            if (findedUser == null || !findedUser.Password.Equals(password))
            {
                throw new UserNotFoundException();
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenKey = Encoding.UTF8.GetBytes(_configuration["JWT:Key"]);
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, findedUser.Id.ToString()),
                new Claim(ClaimTypes.Name, findedUser.Name)
            };
            var creds = new SigningCredentials(new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256Signature);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                NotBefore = DateTime.UtcNow,
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = creds
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public async Task RegistrateUserAsync(string name, string password)
        {
            var findedUser = await _libraryContext.Users.FirstOrDefaultAsync(x => x.Name.Equals(name));
            if (findedUser != null)
            {
                throw new UserAlreadyExistsException();
            }

            await _libraryContext.Users.AddAsync(new User { Name = name, Password = password });
            await _libraryContext.SaveChangesAsync();
        }
    }
}
