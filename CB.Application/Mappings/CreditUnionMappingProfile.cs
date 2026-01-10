

using CB.Application.DTOs.CreditUnion;
using CB.Core.Entities;

namespace CB.Application.Mappings
{
    public class CreditUnionMappingProfile : MappingProfile
    {
        public CreditUnionMappingProfile() : base()
        {
            CreateMap<CreditUnion, CreditUnionGetDTO>()
                .ForMember(dest => dest.Titles, opt =>
                {
                    opt.MapFrom(src => src.Translations.ToDictionary(
                        t => t.Language.Code,
                        t => t.Title
                    ));
                });


            CreateMap<CreditUnionCreateDTO, CreditUnion>()
                .ForMember(dest => dest.Translations, src => src.Ignore());

            CreateMap<CreditUnionEditDTO, CreditUnion>()
                .ForMember(dest => dest.Translations, src => src.Ignore());

        }
    }
}
