

using CB.Application.DTOs.PaymentStatistic;
using CB.Application.Mappings.Resolvers;
using CB.Core.Entities;

namespace CB.Application.Mappings
{
    public class PaymentStatisticMappingProfile : MappingProfile
    {
        public PaymentStatisticMappingProfile() : base()
        {
            CreateMap<PaymentStatistic, PaymentStatisticGetDTO>()
                .ForMember(dest => dest.File, opt => opt.MapFrom<GenericResolver<PaymentStatistic, PaymentStatisticGetDTO>>())
                .ForMember(dest => dest.Titles, opt =>
                {
                    opt.MapFrom(src => src.Translations.ToDictionary(
                        t => t.Language.Code,
                        t => t.Title
                    ));
                });


            CreateMap<PaymentStatisticCreateDTO, PaymentStatistic>()
                .ForMember(dest => dest.Translations, src => src.Ignore())
                .ForMember(dest => dest.FileType, src => src.Ignore())
                .ForMember(dest => dest.File, src => src.Ignore());

            CreateMap<PaymentStatisticEditDTO, PaymentStatistic>()
                .ForMember(dest => dest.Translations, src => src.Ignore())
                .ForMember(dest => dest.FileType, src => src.Ignore())
                .ForMember(dest => dest.File, src => src.Ignore());

        }
    }
}
