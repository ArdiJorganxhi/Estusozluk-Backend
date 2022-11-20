using System;
using EstuSozluk.API.Models.Dtos;

namespace EstuSozluk.API.Services.Abstracts
{
    public interface IAuthenticationService
    {
        public string CreateToken(UserLoginDto userLoginDto);
        public void VerifyToken();
    }
}

