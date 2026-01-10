

using CB.Application.DTOs.ReceptionCitizenLink;
using CB.Application.Mappings.Resolvers;
using CB.Core.Entities;

namespace CB.Application.Mappings
{
    public class ReceptionCitizenLinkMappingProfile : MappingProfile
    {
        public ReceptionCitizenLinkMappingProfile() : base()
        {
            CreateMap<ReceptionCitizenLink, ReceptionCitizenLinkGetDTO>()
                .ForMember(dest => dest.ReceptionCitizenCategoryTitle, src => src.Ignore())
                .ForMember(dest => dest.Titles, opt =>
                {
                    opt.MapFrom(src => src.Translations.ToDictionary(
                        t => t.Language.Code,
                        t => t.Title
                    ));
                });


            CreateMap<ReceptionCitizenLinkCreateDTO, ReceptionCitizenLink>()
                .ForMember(dest => dest.Translations, src => src.Ignore());

            CreateMap<ReceptionCitizenLinkEditDTO, ReceptionCitizenLink>()
                .ForMember(dest => dest.Translations, src => src.Ignore());

        }
    }
}
