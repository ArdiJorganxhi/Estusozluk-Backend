using EstuSozluk.API.Models;
using EstuSozluk.API.Repositories;
using EstuSozluk.API.Services.Abstracts;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace EstuSozluk.API.Middlewares
{
    public class JwtMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IConfiguration _appSettings;

        public JwtMiddleware(RequestDelegate next, IConfiguration appSettings)
        {
            _next = next;
            _appSettings = appSettings;
        }

        public async Task Invoke(HttpContext context, ILoginService LoginService, EstuSozlukContext estuSozlukContext)
        {
            Console.WriteLine("Aaaaaaaaaa");
            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

            if (token != null)
                attachUserToContext(context, LoginService, token, estuSozlukContext);

            await _next(context);

            
        }

        private void attachUserToContext(HttpContext context, ILoginService LoginService, string token, EstuSozlukContext estuSozlukContext)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(_appSettings["Jwt:Key"]);
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    // set clockskew to zero so tokens expire exactly at token expiration time (instead of 5 minutes later)
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);

                var jwtToken = (JwtSecurityToken)validatedToken;

                var userEmail = jwtToken.Claims.First(x => x.Type == "email").Value;
                var userUserName = jwtToken.Claims.First(x => x.Type == "username").Value;
                var userCanpost = jwtToken.Claims.First(x => x.Type == "canpost").Value;

                Console.WriteLine(userEmail);
                Console.WriteLine(userUserName);
                Console.WriteLine(userCanpost);

                User user = estuSozlukContext.Users.Where(e => e.email == userEmail && e.username == userUserName).First();     
                
                if(user.email == userEmail && userUserName == user.username)
                {
                    // attach user to context on successful jwt validation
                    context.Items["User"] = user;
                }
                else
                {
                    context.Items["User"] = null; 
                }



            }
            catch
            {
                // do nothing if jwt validation fails
                // user is not attached to context so request won't have access to secure routes
            }
        }
    }
}

