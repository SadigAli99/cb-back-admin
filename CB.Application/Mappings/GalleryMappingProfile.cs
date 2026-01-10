

using CB.Application.DTOs.Gallery;
using CB.Application.Mappings.Resolvers;
using CB.Core.Entities;

namespace CB.Application.Mappings
{
    public class GalleryMappingProfile : MappingProfile
    {
        public GalleryMappingProfile() : base()
        {
            CreateMap<Gallery, GalleryGetDTO>()
                .ForMember(dest => dest.Image, opt => opt.MapFrom<GenericResolver<Gallery, GalleryGetDTO>>())
                .ForMember(dest => dest.Images, src => src.Ignore())
                .ForMember(dest => dest.Titles, opt =>
                {
                    opt.MapFrom(src => src.Translations.ToDictionary(
                        t => t.Language.Code,
                        t => t.Title
                    ));
                })
                .ForMember(dest => dest.ImageTitles, opt =>
                {
                    opt.MapFrom(src => src.Translations.ToDictionary(
                        t => t.Language.Code,
                        t => t.ImageTitle
                    ));
                })
                .ForMember(dest => dest.ImageAlts, opt =>
                {
                    opt.MapFrom(src => src.Translations.ToDictionary(
                        t => t.Language.Code,
                        t => t.ImageAlt
                    ));
                });


            CreateMap<GalleryCreateDTO, Gallery>()
                .ForMember(dest => dest.Translations, src => src.Ignore())
                .ForMember(dest => dest.Images, src => src.Ignore());

            CreateMap<GalleryEditDTO, Gallery>()
                .ForMember(dest => dest.Translations, src => src.Ignore())
                .ForMember(dest => dest.Images, src => src.Ignore());

            CreateMap<GalleryImage, GalleryImageDTO>()
                .ForMember(dest => dest.Image, opt => opt.MapFrom<GenericResolver<GalleryImage, GalleryImageDTO>>());

        }
    }
}
