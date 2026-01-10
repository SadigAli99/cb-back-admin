

using CB.Application.DTOs.LegalActStatistic;
using CB.Application.Mappings.Resolvers;
using CB.Core.Entities;

namespace CB.Application.Mappings
{
    public class LegalActStatisticMappingProfile : MappingProfile
    {
        public LegalActStatisticMappingProfile() : base()
        {
            CreateMap<LegalActStatistic, LegalActStatisticGetDTO>()
                .ForMember(dest => dest.File, opt => opt.MapFrom<GenericResolver<LegalActStatistic, LegalActStatisticGetDTO>>())
                .ForMember(dest => dest.Titles, opt =>
                {
                    opt.MapFrom(src => src.Translations.ToDictionary(
                        t => t.Language.Code,
                        t => t.Title
                    ));
                });


            CreateMap<LegalActStatisticCreateDTO, LegalActStatistic>()
                .ForMember(dest => dest.Translations, src => src.Ignore())
                .ForMember(dest => dest.FileType, src => src.Ignore())
                .ForMember(dest => dest.File, src => src.Ignore());

            CreateMap<LegalActStatisticEditDTO, LegalActStatistic>()
                .ForMember(dest => dest.Translations, src => src.Ignore())
                .ForMember(dest => dest.FileType, src => src.Ignore())
                .ForMember(dest => dest.File, src => src.Ignore());

        }
    }
}
