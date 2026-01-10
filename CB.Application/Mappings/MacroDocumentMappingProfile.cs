

using CB.Application.DTOs.MacroDocument;
using CB.Application.Mappings.Resolvers;
using CB.Core.Entities;

namespace CB.Application.Mappings
{
    public class MacroDocumentMappingProfile : MappingProfile
    {
        public MacroDocumentMappingProfile() : base()
        {
            CreateMap<MacroDocument, MacroDocumentGetDTO>()
                .ForMember(dest=>dest.Icon, src=>src.MapFrom<GenericResolver<MacroDocument,MacroDocumentGetDTO>>())
                .ForMember(dest => dest.Titles, opt =>
                {
                    opt.MapFrom(src => src.Translations.ToDictionary(
                        t => t.Language.Code,
                        t => t.Title
                    ));
                })
                .ForMember(dest => dest.Texts, opt =>
                {
                    opt.MapFrom(src => src.Translations.ToDictionary(
                        t => t.Language.Code,
                        t => t.Text
                    ));
                });


            CreateMap<MacroDocumentCreateDTO, MacroDocument>()
                .ForMember(dest => dest.Translations, src => src.Ignore());

            CreateMap<MacroDocumentEditDTO, MacroDocument>()
                .ForMember(dest => dest.Translations, src => src.Ignore());

        }
    }
}
