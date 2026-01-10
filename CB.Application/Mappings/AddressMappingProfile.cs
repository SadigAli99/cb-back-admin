
using CB.Application.DTOs.Address;
using CB.Core.Entities;

namespace CB.Application.Mappings
{
    public class AddressMappingProfile : MappingProfile
    {
        public AddressMappingProfile() : base()
        {
            CreateMap<Address, AddressGetDTO>()
                    .ForMember(dest => dest.Titles, opt =>
                        opt.MapFrom(src => src.Translations
                            .ToDictionary(
                                t => t.Language.Code,
                                t => t.Title
                            )
                        )
                    )
                    .ForMember(dest => dest.Texts, opt =>
                        opt.MapFrom(src => src.Translations
                            .ToDictionary(
                                t => t.Language.Code,
                                t => t.Text
                            )
                        )
                    );

            CreateMap<AddressCreateDTO, Address>()
                .ForMember(dest => dest.Translations, opt => opt.Ignore());

            CreateMap<AddressEditDTO, Address>()
                .ForMember(dest => dest.Translations, opt => opt.Ignore());
        }
    }
}
