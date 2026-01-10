

using CB.Application.DTOs.CentralBankCooperation;
using CB.Application.Mappings.Resolvers;
using CB.Core.Entities;

namespace CB.Application.Mappings
{
    public class CentralBankCooperationMappingProfile : MappingProfile
    {
        public CentralBankCooperationMappingProfile() : base()
        {
            CreateMap<CentralBankCooperation, CentralBankCooperationGetDTO>()
                .ForMember(dest => dest.Titles, opt =>
                {
                    opt.MapFrom(src => src.Translations.ToDictionary(
                        t => t.Language.Code,
                        t => t.Title
                    ));
                });


            CreateMap<CentralBankCooperationCreateDTO, CentralBankCooperation>()
                .ForMember(dest => dest.Translations, src => src.Ignore());

            CreateMap<CentralBankCooperationEditDTO, CentralBankCooperation>()
                .ForMember(dest => dest.Translations, src => src.Ignore());

        }
    }
}
