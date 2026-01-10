

using CB.Application.DTOs.FraudStatistic;
using CB.Application.Mappings.Resolvers;
using CB.Core.Entities;

namespace CB.Application.Mappings
{
    public class FraudStatisticMappingProfile : MappingProfile
    {
        public FraudStatisticMappingProfile() : base()
        {
            CreateMap<FraudStatistic, FraudStatisticGetDTO>()
                .ForMember(dest => dest.File, opt => opt.MapFrom<GenericResolver<FraudStatistic, FraudStatisticGetDTO>>())
                .ForMember(dest => dest.Titles, opt =>
                {
                    opt.MapFrom(src => src.Translations.ToDictionary(
                        t => t.Language.Code,
                        t => t.Title
                    ));
                });


            CreateMap<FraudStatisticCreateDTO, FraudStatistic>()
                .ForMember(dest => dest.Translations, src => src.Ignore())
                .ForMember(dest => dest.FileType, src => src.Ignore())
                .ForMember(dest => dest.File, src => src.Ignore());

            CreateMap<FraudStatisticEditDTO, FraudStatistic>()
                .ForMember(dest => dest.Translations, src => src.Ignore())
                .ForMember(dest => dest.FileType, src => src.Ignore())
                .ForMember(dest => dest.File, src => src.Ignore());

        }
    }
}
