
using CB.Application.DTOs.ReviewApplication;
using CB.Core.Entities;

namespace CB.Application.Mappings
{
    public class ReviewApplicationMappingProfile : MappingProfile
    {
        public ReviewApplicationMappingProfile() : base()
        {
            CreateMap<ReviewApplication, ReviewApplicationGetDTO>()
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

            CreateMap<ReviewApplicationCreateDTO, ReviewApplication>()
                .ForMember(dest => dest.Translations, opt => opt.Ignore());

            CreateMap<ReviewApplicationEditDTO, ReviewApplication>()
                .ForMember(dest => dest.Translations, opt => opt.Ignore());
        }
    }
}
