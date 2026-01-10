

using CB.Application.DTOs.MonetaryPolicyDirection;
using CB.Application.Mappings.Resolvers;
using CB.Core.Entities;

namespace CB.Application.Mappings
{
    public class MonetaryPolicyDirectionMappingProfile : MappingProfile
    {
        public MonetaryPolicyDirectionMappingProfile() : base()
        {
            CreateMap<MonetaryPolicyDirection, MonetaryPolicyDirectionGetDTO>()
                .ForMember(dest => dest.File, opt => opt.MapFrom<GenericResolver<MonetaryPolicyDirection, MonetaryPolicyDirectionGetDTO>>())
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


            CreateMap<MonetaryPolicyDirectionCreateDTO, MonetaryPolicyDirection>()
                .ForMember(dest => dest.Translations, src => src.Ignore())
                .ForMember(dest => dest.FileType, src => src.Ignore())
                .ForMember(dest => dest.File, src => src.Ignore());

            CreateMap<MonetaryPolicyDirectionEditDTO, MonetaryPolicyDirection>()
                .ForMember(dest => dest.Translations, src => src.Ignore())
                .ForMember(dest => dest.FileType, src => src.Ignore())
                .ForMember(dest => dest.File, src => src.Ignore());

        }
    }
}
