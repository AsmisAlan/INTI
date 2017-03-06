using AutoMapper;
using GeoUsers.Services.Automapper.Profiles;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeoUsers.Services
{
    public class AutoMapperConfiguration
    {
        public static IMapper ConfigureAutomapper()
        {
            //Configure Automapper
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<EntitiesProfile>();
            });

            return config.CreateMapper();
        }
    }
}
