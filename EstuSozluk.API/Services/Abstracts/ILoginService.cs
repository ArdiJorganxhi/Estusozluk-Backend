using System;
using EstuSozluk.API.Models;
using EstuSozluk.API.Models.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace EstuSozluk.API.Services.Abstracts
{
    public interface ILoginService
    {

        public bool CheckIfUserExists(String username);
        public User SaveUser(UserRegistrationDto user);
        public object Login(UserLoginDto UserLoginDto);




    }
}

