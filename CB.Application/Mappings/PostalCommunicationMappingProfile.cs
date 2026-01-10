

using CB.Application.DTOs.PostalCommunication;
using CB.Application.Mappings.Resolvers;
using CB.Core.Entities;

namespace CB.Application.Mappings
{
    public class PostalCommunicationMappingProfile : MappingProfile
    {
        public PostalCommunicationMappingProfile() : base()
        {
            CreateMap<PostalCommunication, PostalCommunicationGetDTO>()
                .ForMember(dest => dest.File, opt => opt.MapFrom<GenericResolver<PostalCommunication, PostalCommunicationGetDTO>>())
                .ForMember(dest => dest.Titles, opt =>
                {
                    opt.MapFrom(src => src.Translations.ToDictionary(
                        t => t.Language.Code,
                        t => t.Title
                    ));
                });


            CreateMap<PostalCommunicationCreateDTO, PostalCommunication>()
                .ForMember(dest => dest.Translations, src => src.Ignore())
                .ForMember(dest => dest.FileType, src => src.Ignore())
                .ForMember(dest => dest.File, src => src.Ignore());

            CreateMap<PostalCommunicationEditDTO, PostalCommunication>()
                .ForMember(dest => dest.Translations, src => src.Ignore())
                .ForMember(dest => dest.FileType, src => src.Ignore())
                .ForMember(dest => dest.File, src => src.Ignore());

        }
    }
}
