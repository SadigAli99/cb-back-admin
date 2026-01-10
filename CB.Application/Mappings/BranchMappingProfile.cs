
using CB.Application.DTOs.Branch;
using CB.Core.Entities;

namespace CB.Application.Mappings
{
    public class BranchMappingProfile : MappingProfile
    {
        public BranchMappingProfile() : base()
        {
            CreateMap<Branch, BranchGetDTO>()
                    .ForMember(dest => dest.Department, src => src.Ignore())
                    .ForMember(dest => dest.Titles, opt =>
                        opt.MapFrom(src => src.Translations
                            .ToDictionary(
                                t => t.Language.Code,
                                t => t.Title
                            )
                        )
                    );

            CreateMap<BranchCreateDTO, Branch>()
                .ForMember(dest => dest.Translations, opt => opt.Ignore());

            CreateMap<BranchEditDTO, Branch>()
                .ForMember(dest => dest.Translations, opt => opt.Ignore());
        }
    }
}
