
using CB.Application.DTOs.Department;
using CB.Core.Entities;

namespace CB.Application.Mappings
{
    public class DepartmentMappingProfile : MappingProfile
    {
        public DepartmentMappingProfile() : base()
        {
            CreateMap<Department, DepartmentGetDTO>()
                    .ForMember(dest => dest.Titles, opt =>
                        opt.MapFrom(src => src.Translations
                            .ToDictionary(
                                t => t.Language.Code,
                                t => t.Title
                            )
                        )
                    );

            CreateMap<DepartmentCreateDTO, Department>()
                .ForMember(dest => dest.Translations, opt => opt.Ignore());

            CreateMap<DepartmentEditDTO, Department>()
                .ForMember(dest => dest.Translations, opt => opt.Ignore());
        }
    }
}
