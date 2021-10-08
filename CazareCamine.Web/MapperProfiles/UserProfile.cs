using AutoMapper;
using CazareCamine.Data.Entities.DTO;
using CazareCamine.Data.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CazareCamine.Web.MapperProfiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<UserForRegistrationDTO, UserModel>()
                .ForMember(u => u.UserName, opt => opt.MapFrom(x => x.Email)); ;
        }
    }
}
