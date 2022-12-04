using System;
using EstuSozluk.API.Models.Dtos;

namespace EstuSozluk.API.Models.Mappers
{
    public class FollowshipMapper
    {

        public FollowshipMapper()
        {
            
        }
        public static Followships FollowshipsMapper(FollowshipsDto followshipsDto)
        {

            Followships followships = new Followships();
                    
            followships.follower = followshipsDto.follower;
            followships.followed = followshipsDto.followed;
           
        

            return followships;
        }
    }
}