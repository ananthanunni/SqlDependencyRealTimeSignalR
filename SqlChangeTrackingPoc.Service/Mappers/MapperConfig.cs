using AutoMapper;
using SqlChangeTrackingPoc.Entity.Models;
using SqlChangeTrackingPoc.Models.DevTest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqlChangeTrackingPoc.Service.Mappers
{
    public static class MapperConfig
    {
        static MapperConfig()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<DevTest, DevTestModel>();
                cfg.CreateMap<DevTestModel,DevTest>();

            });

            Mapping = config.CreateMapper();
        }

        public static IMapper Mapping = null;
    }
}
