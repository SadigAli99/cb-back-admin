
using CB.Application.DTOs.TrainingJournalist;
using CB.Application.Mappings.Resolvers;
using CB.Core.Entities;

namespace CB.Application.Mappings
{
    public class TrainingJournalistMappingProfile : MappingProfile
    {
        public TrainingJournalistMappingProfile()
        {
            CreateMap<TrainingJournalist, TrainingJournalistGetDTO>()
                .ForMember(dest => dest.Descriptions, opt =>
                    opt.MapFrom(src => src.Translations
                        .ToDictionary(
                            t => t.Language.Code,
                            t => t.Description
                        )
                    )
                )
                .ForMember(dest => dest.Titles, opt =>
                    opt.MapFrom(src => src.Translations
                        .ToDictionary(
                            t => t.Language.Code,
                            t => t.Title
                        )
                    )
                );

            CreateMap<TrainingJournalistPostDTO, TrainingJournalist>()
                .ForMember(dest => dest.Translations, opt => opt.Ignore())
                .ForMember(dest => dest.Images, src => src.Ignore())
                .ReverseMap();

            CreateMap<TrainingJournalistImage, TrainingJournalistImageDTO>()
                .ForMember(dest => dest.Image, opt => opt.MapFrom<GenericResolver<TrainingJournalistImage, TrainingJournalistImageDTO>>());
        }
    }
}
