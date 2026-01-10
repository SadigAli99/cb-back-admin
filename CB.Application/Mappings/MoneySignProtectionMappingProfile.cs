

using CB.Application.DTOs.MoneySignProtection;
using CB.Core.Entities;

namespace CB.Application.Mappings
{
    public class MoneySignProtectionMappingProfile : MappingProfile
    {
        public MoneySignProtectionMappingProfile() : base()
        {
            CreateMap<MoneySignProtection, MoneySignProtectionGetDTO>()
                .ForMember(dest => dest.Image, opt => opt.Ignore())
                .ForMember(dest=>dest.MoneySignHistoryTitle, opt=>opt.Ignore());


            CreateMap<MoneySignProtectionCreateDTO, MoneySignProtection>();

            CreateMap<MoneySignProtectionEditDTO, MoneySignProtection>();

        }
    }
}
