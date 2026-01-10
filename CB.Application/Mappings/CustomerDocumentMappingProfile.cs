

using CB.Application.DTOs.CustomerDocument;
using CB.Application.Mappings.Resolvers;
using CB.Core.Entities;

namespace CB.Application.Mappings
{
    public class CustomerDocumentMappingProfile : MappingProfile
    {
        public CustomerDocumentMappingProfile() : base()
        {
            CreateMap<CustomerDocument, CustomerDocumentGetDTO>()
                .ForMember(dest => dest.Titles, opt =>
                {
                    opt.MapFrom(src => src.Translations.ToDictionary(
                        t => t.Language.Code,
                        t => t.Title
                    ));
                })
                .ForMember(dest => dest.Descriptions, opt =>
                {
                    opt.MapFrom(src => src.Translations.ToDictionary(
                        t => t.Language.Code,
                        t => t.Description
                    ));
                });


            CreateMap<CustomerDocumentCreateDTO, CustomerDocument>()
                .ForMember(dest => dest.Translations, src => src.Ignore());

            CreateMap<CustomerDocumentEditDTO, CustomerDocument>()
                .ForMember(dest => dest.Translations, src => src.Ignore());

        }
    }
}
