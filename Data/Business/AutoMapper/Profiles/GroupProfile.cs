using AutoMapper;
using Data.Models.DAO;
using Data.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Business.AutoMapper.Profiles
{
    /// <summary>
    /// GroupProfile.
    /// </summary>
    internal class GroupProfile : Profile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GroupProfile" /> class.
        /// </summary>
        public GroupProfile()
        {
            CreateMap<GroupDAO, GroupDTO>().ReverseMap();
        }
    }
}