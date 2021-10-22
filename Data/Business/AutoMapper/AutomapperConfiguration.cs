using AutoMapper;
using Data.Business.AutoMapper.Profiles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Business.AutoMapper
{
    /// <summary>
    /// AutomapperConfiguration.
    /// </summary>
    public static class AutomapperConfiguration
    {
        /// <summary>
        /// Configures this instance.
        /// </summary>
        public static IMapper Configure()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<GroupProfile>();
                cfg.AddProfile<PersonProfile>();
            });
            config.AssertConfigurationIsValid();
            return config.CreateMapper();
        }
    }
}