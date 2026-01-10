
using CB.Application.DTOs.Position;
using CB.Core.Entities;

namespace CB.Application.Mappings
{
    public class PositionMappingProfile : MappingProfile
    {
        public PositionMappingProfile() : base()
        {
            CreateMap<Position, PositionGetDTO>()
                    .ForMember(dest => dest.Branch, src => src.Ignore())
                    .ForMember(dest => dest.Titles, opt =>
                        opt.MapFrom(src => src.Translations
                            .ToDictionary(
                                t => t.Language.Code,
                                t => t.Title
                            )
                        )
                    );

            CreateMap<PositionCreateDTO, Position>()
                .ForMember(dest => dest.Translations, opt => opt.Ignore());

            CreateMap<PositionEditDTO, Position>()
                .ForMember(dest => dest.Translations, opt => opt.Ignore());
        }
    }
}
