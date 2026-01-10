

using CB.Application.DTOs.MonetaryPolicyInstrument;
using CB.Application.Mappings.Resolvers;
using CB.Core.Entities;

namespace CB.Application.Mappings
{
    public class MonetaryPolicyInstrumentMappingProfile : MappingProfile
    {
        public MonetaryPolicyInstrumentMappingProfile() : base()
        {
            CreateMap<MonetaryPolicyInstrument, MonetaryPolicyInstrumentGetDTO>()
                .ForMember(dest => dest.File, opt => opt.MapFrom<GenericResolver<MonetaryPolicyInstrument, MonetaryPolicyInstrumentGetDTO>>())
                .ForMember(dest => dest.Titles, opt =>
                {
                    opt.MapFrom(src => src.Translations.ToDictionary(
                        t => t.Language.Code,
                        t => t.Title
                    ));
                })
                .ForMember(dest => dest.CoverTitles, opt =>
                {
                    opt.MapFrom(src => src.Translations.ToDictionary(
                        t => t.Language.Code,
                        t => t.CoverTitle
                    ));
                });


            CreateMap<MonetaryPolicyInstrumentCreateDTO, MonetaryPolicyInstrument>()
                .ForMember(dest => dest.Translations, src => src.Ignore())
                .ForMember(dest => dest.FileType, src => src.Ignore())
                .ForMember(dest => dest.File, src => src.Ignore());

            CreateMap<MonetaryPolicyInstrumentEditDTO, MonetaryPolicyInstrument>()
                .ForMember(dest => dest.Translations, src => src.Ignore())
                .ForMember(dest => dest.FileType, src => src.Ignore())
                .ForMember(dest => dest.File, src => src.Ignore());

        }
    }
}
