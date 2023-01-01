using System;
using System.Collections.Generic;
using System.Linq;
using EstuSozluk.API.Models;
using EstuSozluk.API.Models.Dtos;
using EstuSozluk.API.Models.Mappers;
using EstuSozluk.API.Repositories;
using EstuSozluk.API.Services.Abstracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Abstractions;

namespace EstuSozluk.API.Services.Concretes
{
    public class LoginService : ILoginService
    {

        EstuSozlukContext _estuSozlukContext;
        IAuthenticationService _authenticationService;

        public LoginService(EstuSozlukContext context, IAuthenticationService authenticationService)
        {
            _estuSozlukContext = context;
            _authenticationService = authenticationService;
        }

        public User GetUser(String username)
        {

            var checkIfUserExists = _estuSozlukContext.Users.Where(e => e.username == username).
                FirstOrDefault();

            if (checkIfUserExists != null)
            {
                return checkIfUserExists;
            }
            else
            {
                return null;
            }




        }

        //public object GetUserByUsername(string Username)
        //{
        //    // User user = _estuSozlukContext.Users.Include(e => e.Following).Include(e => e.entries).Where(e => e.username == Username).First();

        //    var userData = _estuSozlukContext.Set<User>().Where(e => e.username == Username)
        //       .Select(e => new
        //       {
        //           e.username,
        //           e.email,
        //           e.permissions,
        //           Followers = e.Followed.Select(e => e.User1.username).ToList(),
        //           Following = e.Following.Select(e => e.User2.username).ToList(),
        //           LikedEntries = e.LikedEntries.Select(e => new { e.entry.entryid, e.entry.content }).ToList(),
        //           DisLikedEntries = e.DislikedEntries.Select(e => new { e.entry.entryid, e.entry.content }).ToList()
        //       }).First();

        //    IEnumerable<string> badies = userData.Following.Intersect(userData.Followers);

        //    return new
        //    {
        //        userData.username,
        //        userData.email,
        //        userData.permissions,
        //        userData.Following,
        //        FollowerCount = userData.Followers.Count,
        //        FollowedCount = userData.Following.Count,
        //        LikedEntries = userData.LikedEntries,
        //        DisLikedEntries = userData.DisLikedEntries,
        //        badies = badies,
        //        badieCount = badies.Count()
        //    };
        //}

        public object Login(UserLoginDto UserLoginDto)
        {


            var userData = _estuSozlukContext.Set<User>().Where(e => e.username == UserLoginDto.username && e.password == UserLoginDto.password)
               .Select(e => new
               {
                   e.userid,
                   e.username,
                   e.email,
                   e.permissions,
                   Followers = e.Followed.Select(e => e.User1.username).ToList(),
                   Following = e.Following.Select(e => e.User2.username).ToList(),
                   LikedEntries = e.LikedEntries.Select(e => new { e.entry.titlename, e.entry.content }).ToList(),
                   DisLikedEntries = e.DislikedEntries.Select(e => new { e.entry.titlename, e.entry.content }).ToList()
               }).First();

            IEnumerable<string> badies = userData.Following.Intersect(userData.Followers);

            string token = _authenticationService.CreateToken(UserLoginDto);

            return new
            {
                userData.userid,
                userData.username,
                userData.email,
                userData.permissions,
                userData.Following,
                FollowerCount = userData.Followers.Count,
                FollowedCount = userData.Following.Count,
                LikedEntries = userData.LikedEntries,
                DisLikedEntries = userData.DisLikedEntries,
                badies = badies,
                badieCount = badies.Count(),
                token = token
            };
        }

        public User SaveUser(UserRegistrationDto user)
        {
            User userToSave = UserMapper.GetUserFromUserRegistrationDto(user);
            _estuSozlukContext.Users.Add(userToSave);
            _estuSozlukContext.SaveChanges();
            return userToSave;
        }

        public Followships Follow(FollowshipsDto followshipsDto)
        {

            Followships followshipToSave = FollowshipMapper.FollowshipsMapper(followshipsDto);

            _estuSozlukContext.Followships.Add(followshipToSave);
            _estuSozlukContext.SaveChanges();

            return followshipToSave;

            
        }

        public object UpdateUser(int userid, UserUpdateDto UserUpdateDto)
        {
            User userUpdate = UserMapper.GetUserFromUserUpdateDto(UserUpdateDto);
            var checkUser = _estuSozlukContext.Users.Where(e => e.userid == userid).FirstOrDefault();

            if (checkUser != null)
            {
                if (UserUpdateDto.username != null)
                {
                    checkUser.username = UserUpdateDto.username;
                } 
                
                if (UserUpdateDto.email != null)
                {
                    checkUser.email = UserUpdateDto.email;
                }
                
                _estuSozlukContext.SaveChanges();


                return checkUser;
            }
            
            
            
            

            return "User not updated!";


        }
        
        
    }
}

