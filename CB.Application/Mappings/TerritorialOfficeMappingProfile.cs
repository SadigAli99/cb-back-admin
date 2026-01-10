

using CB.Application.DTOs.TerritorialOffice;
using CB.Core.Entities;

namespace CB.Application.Mappings
{
    public class TerritorialOfficeMappingProfile : MappingProfile
    {
        public TerritorialOfficeMappingProfile() : base()
        {
            CreateMap<TerritorialOffice, TerritorialOfficeGetDTO>()
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


            CreateMap<TerritorialOfficeCreateDTO, TerritorialOffice>()
                .ForMember(dest => dest.Translations, src => src.Ignore());

            CreateMap<TerritorialOfficeEditDTO, TerritorialOffice>()
                .ForMember(dest => dest.Translations, src => src.Ignore());

        }
    }
}
