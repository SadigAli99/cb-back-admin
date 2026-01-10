
using CB.Application.DTOs.Phone;
using CB.Core.Entities;

namespace CB.Application.Mappings
{
    public class PhoneMappingProfile : MappingProfile
    {
        public PhoneMappingProfile() : base()
        {
            CreateMap<Phone, PhoneGetDTO>()
                    .ForMember(dest => dest.Titles, opt =>
                        opt.MapFrom(src => src.Translations
                            .ToDictionary(
                                t => t.Language.Code,
                                t => t.Title
                            )
                        )
                    );

            CreateMap<PhoneCreateDTO, Phone>()
                .ForMember(dest => dest.Translations, opt => opt.Ignore());

            CreateMap<PhoneEditDTO, Phone>()
                .ForMember(dest => dest.Translations, opt => opt.Ignore());
        }
    }
}
