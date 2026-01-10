

using CB.Application.DTOs.PaymentRight;
using CB.Application.Mappings.Resolvers;
using CB.Core.Entities;

namespace CB.Application.Mappings
{
    public class PaymentRightMappingProfile : MappingProfile
    {
        public PaymentRightMappingProfile() : base()
        {
            CreateMap<PaymentRight, PaymentRightGetDTO>()
                .ForMember(dest => dest.File, opt => opt.MapFrom<GenericResolver<PaymentRight, PaymentRightGetDTO>>())
                .ForMember(dest => dest.Titles, opt =>
                {
                    opt.MapFrom(src => src.Translations.ToDictionary(
                        t => t.Language.Code,
                        t => t.Title
                    ));
                });


            CreateMap<PaymentRightCreateDTO, PaymentRight>()
                .ForMember(dest => dest.Translations, src => src.Ignore())
                .ForMember(dest => dest.FileType, src => src.Ignore())
                .ForMember(dest => dest.File, src => src.Ignore());

            CreateMap<PaymentRightEditDTO, PaymentRight>()
                .ForMember(dest => dest.Translations, src => src.Ignore())
                .ForMember(dest => dest.FileType, src => src.Ignore())
                .ForMember(dest => dest.File, src => src.Ignore());

        }
    }
}
