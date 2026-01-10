using CB.Application.DTOs.CitizenApplication;
using CB.Core.Entities;

namespace CB.Application.Mappings
{
    public class CitizenApplicationMappingProfile : MappingProfile
    {
        public CitizenApplicationMappingProfile()
        {
            /// <summary>
            /// DTOs For CitizenApplications
            /// </summary>
            /// <typeparam name="CitizenApplication"></typeparam>
            /// <typeparam name="CitizenApplicationGetDTO"></typeparam>
            /// <returns></returns>
            CreateMap<CitizenApplication, CitizenApplicationGetDTO>()
                .ForMember(x => x.CitizenApplicationCategoryTitle, src => src.Ignore());
            CreateMap<CitizenApplicationCreateDTO, CitizenApplication>()
                .ForMember(dest => dest.TotalCount, src => src.Ignore());
            CreateMap<CitizenApplicationEditDTO, CitizenApplication>()
                .ForMember(dest => dest.TotalCount, src => src.Ignore());
        }
    }
}
