using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CB.Application.DTOs.PercentCorridor;
using CB.Application.Mappings.Resolvers;
using CB.Core.Entities;

namespace CB.Application.Mappings
{
    public class PercentCorridorMappingProfile : MappingProfile
    {
        public PercentCorridorMappingProfile()
        {
            /// <summary>
            /// DTOs For PercentCorridors
            /// </summary>
            /// <typeparam name="PercentCorridor"></typeparam>
            /// <typeparam name="PercentCorridorGetDTO"></typeparam>
            /// <returns></returns>
            CreateMap<PercentCorridor, PercentCorridorGetDTO>()
                .ForMember(x => x.PercentCategoryTitle, src => src.Ignore());
            CreateMap<PercentCorridorCreateDTO, PercentCorridor>();
            CreateMap<PercentCorridorCreateDTO, PercentCorridorEditDTO>();
            CreateMap<PercentCorridorEditDTO, PercentCorridor>().ReverseMap();
        }
    }
}
