using AutoMapper;
using Data.Models.DAO;
using Data.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Data.Business.AutoMapper.Profiles
{
    /// <summary>
    /// PersonProfile.
    /// </summary>
    public class PersonProfile : Profile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PersonProfile" /> class.
        /// </summary>
        public PersonProfile()
        {
            CreateMap<PersonDAO, PersonDTO>().ReverseMap();
        }
    }
}