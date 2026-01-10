
using CB.Application.DTOs.ParticipantCategory;
using CB.Core.Entities;

namespace CB.Application.Mappings
{
    public class ParticipantCategoryMappingProfile : MappingProfile
    {
        public ParticipantCategoryMappingProfile() : base()
        {
            CreateMap<ParticipantCategory, ParticipantCategoryGetDTO>()
                    .ForMember(dest => dest.Titles, opt =>
                        opt.MapFrom(src => src.Translations
                            .ToDictionary(
                                t => t.Language.Code,
                                t => t.Title
                            )
                        )
                    );

            CreateMap<ParticipantCategoryCreateDTO, ParticipantCategory>()
                .ForMember(dest => dest.Translations, opt => opt.Ignore());

            CreateMap<ParticipantCategoryEditDTO, ParticipantCategory>()
                .ForMember(dest => dest.Translations, opt => opt.Ignore());
        }
    }
}
