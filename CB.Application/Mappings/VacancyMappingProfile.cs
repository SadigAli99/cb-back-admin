

using CB.Application.DTOs.Vacancy;
using CB.Application.Mappings.Resolvers;
using CB.Core.Entities;

namespace CB.Application.Mappings
{
    public class VacancyMappingProfile : MappingProfile
    {
        public VacancyMappingProfile() : base()
        {
            CreateMap<Vacancy, VacancyGetDTO>()
                .ForMember(dest => dest.DepartmentTitle, src => src.Ignore())
                .ForMember(dest => dest.BranchTitle, src => src.Ignore())
                .ForMember(dest => dest.PositionTitle, src => src.Ignore())
                .ForMember(dest => dest.Titles, opt =>
                {
                    opt.MapFrom(src => src.Translations.ToDictionary(
                        t => t.Language.Code,
                        t => t.Title
                    ));
                })
                .ForMember(dest => dest.Locations, opt =>
                {
                    opt.MapFrom(src => src.Translations.ToDictionary(
                        t => t.Language.Code,
                        t => t.Location
                    ));
                });


            CreateMap<VacancyCreateDTO, Vacancy>()
                .ForMember(dest => dest.Translations, src => src.Ignore());

            CreateMap<VacancyEditDTO, Vacancy>()
                .ForMember(dest => dest.Translations, src => src.Ignore());

        }
    }
}
