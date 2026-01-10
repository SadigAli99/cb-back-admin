

using CB.Application.DTOs.VacancyDetail;
using CB.Application.Mappings.Resolvers;
using CB.Core.Entities;

namespace CB.Application.Mappings
{
    public class VacancyDetailMappingProfile : MappingProfile
    {
        public VacancyDetailMappingProfile() : base()
        {
            CreateMap<VacancyDetail, VacancyDetailGetDTO>()
                .ForMember(dest => dest.VacancyName, src => src.Ignore())
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


            CreateMap<VacancyDetailCreateDTO, VacancyDetail>()
                .ForMember(dest => dest.Translations, src => src.Ignore());

            CreateMap<VacancyDetailEditDTO, VacancyDetail>()
                .ForMember(dest => dest.Translations, src => src.Ignore());

        }
    }
}
