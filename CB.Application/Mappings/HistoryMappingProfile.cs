
using CB.Application.DTOs.History;
using CB.Core.Entities;

namespace CB.Application.Mappings
{
    public class HistoryMappingProfile : MappingProfile
    {
        public HistoryMappingProfile()
        {
            CreateMap<History, HistoryGetDTO>()
                .ForMember(dest => dest.Titles, opt =>
                    opt.MapFrom(src => src.Translations
                        .ToDictionary(
                            t => t.Language.Code,
                            t => t.Title
                        )
                    )
                )
                .ForMember(dest => dest.Descriptions, opt =>
                    opt.MapFrom(src => src.Translations
                        .ToDictionary(
                            t => t.Language.Code,
                            t => t.Description
                        )
                    )
                );

            CreateMap<HistoryPostDTO, History>()
                .ForMember(dest => dest.Translations, opt => opt.Ignore())
                .ReverseMap();
        }
    }
}
