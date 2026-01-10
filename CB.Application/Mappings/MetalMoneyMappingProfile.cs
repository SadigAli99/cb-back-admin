

using CB.Application.DTOs.MetalMoney;
using CB.Application.Mappings.Resolvers;
using CB.Core.Entities;

namespace CB.Application.Mappings
{
    public class MetalMoneyMappingProfile : MappingProfile
    {
        public MetalMoneyMappingProfile() : base()
        {
            CreateMap<Money, MetalMoneyGetDTO>()
                .ForMember(dest => dest.Image, opt => opt.MapFrom<GenericResolver<Money, MetalMoneyGetDTO>>());


            CreateMap<MetalMoneyCreateDTO, Money>()
                .ForMember(dest => dest.Translations, src => src.Ignore());

            CreateMap<MetalMoneyEditDTO, Money>()
                .ForMember(dest => dest.Translations, src => src.Ignore());

        }
    }
}
