using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace pawbossAPI.Entities
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<UserAdd, User>();
        }
    }
}
