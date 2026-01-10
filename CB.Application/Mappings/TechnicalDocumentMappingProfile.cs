

using CB.Application.DTOs.TechnicalDocument;
using CB.Application.Mappings.Resolvers;
using CB.Core.Entities;

namespace CB.Application.Mappings
{
    public class TechnicalDocumentMappingProfile : MappingProfile
    {
        public TechnicalDocumentMappingProfile() : base()
        {
            CreateMap<TechnicalDocument, TechnicalDocumentGetDTO>()
                .ForMember(dest => dest.File, opt => opt.MapFrom<GenericResolver<TechnicalDocument, TechnicalDocumentGetDTO>>())
                .ForMember(dest => dest.Titles, opt =>
                {
                    opt.MapFrom(src => src.Translations.ToDictionary(
                        t => t.Language.Code,
                        t => t.Title
                    ));
                })
                .ForMember(dest => dest.CoverTitles, opt =>
                {
                    opt.MapFrom(src => src.Translations.ToDictionary(
                        t => t.Language.Code,
                        t => t.CoverTitle
                    ));
                });


            CreateMap<TechnicalDocumentCreateDTO, TechnicalDocument>()
                .ForMember(dest => dest.Translations, src => src.Ignore())
                .ForMember(dest => dest.FileType, src => src.Ignore())
                .ForMember(dest => dest.File, src => src.Ignore());

            CreateMap<TechnicalDocumentEditDTO, TechnicalDocument>()
                .ForMember(dest => dest.Translations, src => src.Ignore())
                .ForMember(dest => dest.FileType, src => src.Ignore())
                .ForMember(dest => dest.File, src => src.Ignore());

        }
    }
}
