using System;
using EstuSozluk.API.Models.Dtos;

namespace EstuSozluk.API.Models.Mappers
{
    public class UserMapper
    {
        public static User GetUserFromUserRegistrationDto(UserRegistrationDto UserRegistrationDto)
        {
            User user = new User();
            user.username = UserRegistrationDto.username;
            user.email = UserRegistrationDto.email;
            user.password = UserRegistrationDto.password;
            user.userroleId = 1;

            return user;
        }

        public static User GetUserFromUserLoginDto(UserLoginDto UserLoginDto)
        {
            User user = new User();

            user.username = UserLoginDto.username;
            user.password = UserLoginDto.password;

            return user;
        }

        public static User GetUserFromUserUpdateDto(UserUpdateDto UserUpdateDto)
        {
            User user = new User();
            user.username = UserUpdateDto.username;
            user.email = UserUpdateDto.email;
            user.password = "";

            return user;
        }
        public UserMapper()
        {
        }
    }
}

