

using CB.Application.DTOs.ReviewApplicationFile;
using CB.Application.Mappings.Resolvers;
using CB.Core.Entities;

namespace CB.Application.Mappings
{
    public class ReviewApplicationFileMappingProfile : MappingProfile
    {
        public ReviewApplicationFileMappingProfile() : base()
        {
            CreateMap<ReviewApplicationFile, ReviewApplicationFileGetDTO>()
                .ForMember(dest => dest.File, opt => opt.MapFrom<GenericResolver<ReviewApplicationFile, ReviewApplicationFileGetDTO>>())
                .ForMember(dest => dest.Titles, opt =>
                {
                    opt.MapFrom(src => src.Translations.ToDictionary(
                        t => t.Language.Code,
                        t => t.Title
                    ));
                });


            CreateMap<ReviewApplicationFileCreateDTO, ReviewApplicationFile>()
                .ForMember(dest => dest.Translations, src => src.Ignore())
                .ForMember(dest => dest.FileType, src => src.Ignore())
                .ForMember(dest => dest.File, src => src.Ignore());

            CreateMap<ReviewApplicationFileEditDTO, ReviewApplicationFile>()
                .ForMember(dest => dest.Translations, src => src.Ignore())
                .ForMember(dest => dest.FileType, src => src.Ignore())
                .ForMember(dest => dest.File, src => src.Ignore());

        }
    }
}
