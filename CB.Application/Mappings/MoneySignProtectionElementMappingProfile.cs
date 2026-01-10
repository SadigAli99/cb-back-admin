using AutoMapper;
using CB.Application.DTOs.MoneySignProtectionElement;
using CB.Application.Mappings.Resolvers;
using CB.Core.Entities;

namespace CB.Application.Mappings
{
    public class MoneySignProtectionElementMappingProfile : Profile
    {
        public MoneySignProtectionElementMappingProfile()
        {
            CreateMap<MoneySignProtectionElement, MoneySignProtectionElementGetDTO>()
                .ForMember(dest => dest.Images, opt => opt.MapFrom(src => src.Images))
                .ForMember(dest => dest.MoneySignHistoryTitle, opt => opt.Ignore())
                .ForMember(dest => dest.Titles, opt =>
                {
                    opt.MapFrom(src => src.Translations.ToDictionary(
                        t => t.Language.Code, t => t.Title ?? string.Empty
                    ));
                })
                .ForMember(dest => dest.Values, opt =>
                {
                    opt.MapFrom(src => src.Translations.ToDictionary(
                        t => t.Language.Code, t => t.Value ?? string.Empty
                    ));
                });

            CreateMap<MoneySignProtectionElementCreateDTO, MoneySignProtectionElement>()
                .ForMember(dest => dest.Translations, src => src.Ignore())
                .ForMember(dest => dest.Images, src => src.Ignore());

            CreateMap<MoneySignProtectionElementEditDTO, MoneySignProtectionElement>()
                .ForMember(dest => dest.Translations, src => src.Ignore())
                .ForMember(dest => dest.Images, src => src.Ignore());

            CreateMap<MoneySignProtectionElementImage, MoneySignProtectionElementImageDTO>()
                .ForMember(dest => dest.Image, opt => opt.MapFrom<GenericResolver<MoneySignProtectionElementImage, MoneySignProtectionElementImageDTO>>());
        }
    }
}
