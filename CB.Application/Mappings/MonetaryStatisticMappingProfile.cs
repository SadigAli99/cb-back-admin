

using CB.Application.DTOs.MonetaryStatistic;
using CB.Application.Mappings.Resolvers;
using CB.Core.Entities;

namespace CB.Application.Mappings
{
    public class MonetaryStatisticMappingProfile : MappingProfile
    {
        public MonetaryStatisticMappingProfile() : base()
        {
            CreateMap<MonetaryStatistic, MonetaryStatisticGetDTO>()
                .ForMember(dest => dest.File, opt => opt.MapFrom<GenericResolver<MonetaryStatistic, MonetaryStatisticGetDTO>>())
                .ForMember(dest => dest.Titles, opt =>
                {
                    opt.MapFrom(src => src.Translations.ToDictionary(
                        t => t.Language.Code,
                        t => t.Title
                    ));
                });


            CreateMap<MonetaryStatisticCreateDTO, MonetaryStatistic>()
                .ForMember(dest => dest.Translations, src => src.Ignore())
                .ForMember(dest => dest.FileType, src => src.Ignore())
                .ForMember(dest => dest.File, src => src.Ignore());

            CreateMap<MonetaryStatisticEditDTO, MonetaryStatistic>()
                .ForMember(dest => dest.Translations, src => src.Ignore())
                .ForMember(dest => dest.FileType, src => src.Ignore())
                .ForMember(dest => dest.File, src => src.Ignore());

        }
    }
}
