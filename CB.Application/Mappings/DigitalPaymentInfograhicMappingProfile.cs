

using CB.Application.DTOs.DigitalPaymentInfograhic;
using CB.Application.Mappings.Resolvers;
using CB.Core.Entities;

namespace CB.Application.Mappings
{
    public class DigitalPaymentInfograhicMappingProfile : MappingProfile
    {
        public DigitalPaymentInfograhicMappingProfile() : base()
        {
            CreateMap<DigitalPaymentInfograhic, DigitalPaymentInfograhicGetDTO>()
                .ForMember(dest => dest.DigitalPaymentInfograhicCategoryTitle, src => src.Ignore())
                .ForMember(dest => dest.Titles, opt =>
                {
                    opt.MapFrom(src => src.Translations.ToDictionary(
                        t => t.Language.Code,
                        t => t.Title
                    ));
                });


            CreateMap<DigitalPaymentInfograhicCreateDTO, DigitalPaymentInfograhic>()
                .ForMember(dest => dest.Translations, src => src.Ignore());

            CreateMap<DigitalPaymentInfograhicEditDTO, DigitalPaymentInfograhic>()
                .ForMember(dest => dest.Translations, src => src.Ignore());

        }
    }
}
