using CB.Application.DTOs.MonetaryIndicator;
using CB.Core.Entities;

namespace CB.Application.Mappings
{
    public class MonetaryIndicatorMappingProfile : MappingProfile
    {
        public MonetaryIndicatorMappingProfile()
        {
            /// <summary>
            /// DTOs For MonetaryIndicators
            /// </summary>
            /// <typeparam name="MonetaryIndicator"></typeparam>
            /// <typeparam name="MonetaryIndicatorGetDTO"></typeparam>
            /// <returns></returns>
            CreateMap<MonetaryIndicator, MonetaryIndicatorGetDTO>()
                .ForMember(x => x.PercentCategoryTitle, src => src.Ignore());
            CreateMap<MonetaryIndicatorCreateDTO, MonetaryIndicator>();
            CreateMap<MonetaryIndicatorCreateDTO, MonetaryIndicatorEditDTO>();
            CreateMap<MonetaryIndicatorEditDTO, MonetaryIndicator>().ReverseMap();
        }
    }
}
