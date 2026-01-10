using CB.Application.DTOs.Inflation;
using CB.Core.Entities;

namespace CB.Application.Mappings
{
    public class InflationMappingProfile : MappingProfile
    {
        public InflationMappingProfile()
        {
            /// <summary>
            /// DTOs For Inflations
            /// </summary>
            /// <typeparam name="Inflation"></typeparam>
            /// <typeparam name="InflationGetDTO"></typeparam>
            /// <returns></returns>
            CreateMap<Inflation, InflationGetDTO>()
                .ForMember(dest => dest.Month, src => src.Ignore());
            CreateMap<InflationCreateDTO, Inflation>();
            CreateMap<InflationCreateDTO, InflationEditDTO>();
            CreateMap<InflationEditDTO, Inflation>().ReverseMap();
        }
    }
}
