

using CB.Application.DTOs.DigitalPayment;
using CB.Application.Mappings.Resolvers;
using CB.Core.Entities;

namespace CB.Application.Mappings
{
    public class DigitalPaymentMappingProfile : MappingProfile
    {
        public DigitalPaymentMappingProfile() : base()
        {
            CreateMap<DigitalPayment, DigitalPaymentGetDTO>()
                .ForMember(dest => dest.File, opt => opt.MapFrom<GenericResolver<DigitalPayment, DigitalPaymentGetDTO>>())
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


            CreateMap<DigitalPaymentCreateDTO, DigitalPayment>()
                .ForMember(dest => dest.Translations, src => src.Ignore())
                .ForMember(dest => dest.FileType, src => src.Ignore())
                .ForMember(dest => dest.File, src => src.Ignore());

            CreateMap<DigitalPaymentEditDTO, DigitalPayment>()
                .ForMember(dest => dest.Translations, src => src.Ignore())
                .ForMember(dest => dest.FileType, src => src.Ignore())
                .ForMember(dest => dest.File, src => src.Ignore());

        }
    }
}
