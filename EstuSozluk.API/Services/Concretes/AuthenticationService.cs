using System;
using EstuSozluk.API.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using EstuSozluk.API.Services.Abstracts;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Configuration;
using EstuSozluk.API.Models.Dtos;
using EstuSozluk.API.Repositories;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace EstuSozluk.API.Services.Concretes
{
    public class AuthenticationService : IAuthenticationService
    {

        private IConfiguration _config;
        EstuSozlukContext _estuSozlukContext;
        public AuthenticationService(IConfiguration configuration, EstuSozlukContext context)
        {
            _estuSozlukContext = context;
            _config = configuration;
        }

        public string CreateToken(UserLoginDto UserLoginDto)
        {

            User user = _estuSozlukContext.Users.Include(q => q.permissions).Where(e => e.username == UserLoginDto.username && e.password == UserLoginDto.password).First();
            
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_config["Jwt:Key"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(
                    new[]
                    { new Claim("username", user.username.ToString()),
                      new Claim("email", user.email.ToString()),
                      new Claim("canpost", user.permissions.canpost == 1 ? "true" : "false" ),
                       }
                    ),
                Expires = DateTime.UtcNow.AddMinutes(5),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public void VerifyToken()
        {
            throw new NotImplementedException();
        }
    }
}

