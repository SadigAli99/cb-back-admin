

using CB.Application.DTOs.DirectorContact;
using CB.Application.Mappings.Resolvers;
using CB.Core.Entities;

namespace CB.Application.Mappings
{
    public class DirectorContactMappingProfile : MappingProfile
    {
        public DirectorContactMappingProfile() : base()
        {
            CreateMap<DirectorContact, DirectorContactGetDTO>()
                .ForMember(dest => dest.DirectorName, opt =>
                {
                    opt.MapFrom(src => src.Director.Translations.Where(x => x.LanguageId == 1).FirstOrDefault().Fullname);
                })
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


            CreateMap<DirectorContactCreateDTO, DirectorContact>()
                .ForMember(dest => dest.Translations, src => src.Ignore());

            CreateMap<DirectorContactEditDTO, DirectorContact>()
                .ForMember(dest => dest.Translations, src => src.Ignore());

        }
    }
}
