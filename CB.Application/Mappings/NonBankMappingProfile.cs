

using CB.Application.DTOs.NonBank;
using CB.Core.Entities;

namespace CB.Application.Mappings
{
    public class NonBankMappingProfile : MappingProfile
    {
        public NonBankMappingProfile() : base()
        {
            CreateMap<NonBank, NonBankGetDTO>()
                .ForMember(dest => dest.Titles, opt =>
                {
                    opt.MapFrom(src => src.Translations.ToDictionary(
                        t => t.Language.Code,
                        t => t.Title
                    ));
                })
                .ForMember(dest => dest.Descriptions, opt =>
                {
                    opt.MapFrom(src => src.Translations.ToDictionary(
                        t => t.Language.Code,
                        t => t.Description
                    ));
                });


            CreateMap<NonBankCreateDTO, NonBank>()
                .ForMember(dest => dest.Translations, src => src.Ignore());

            CreateMap<NonBankEditDTO, NonBank>()
                .ForMember(dest => dest.Translations, src => src.Ignore());

        }
    }
}
