

using CB.Application.DTOs.ManagerContact;
using CB.Core.Entities;

namespace CB.Application.Mappings
{
    public class ManagerContactMappingProfile : MappingProfile
    {
        public ManagerContactMappingProfile() : base()
        {
            CreateMap<ManagerContact, ManagerContactGetDTO>()
                .ForMember(dest => dest.ManagerName, opt =>
                {
                    opt.MapFrom(src => src.Manager.Translations.Where(x => x.LanguageId == 1).FirstOrDefault().Fullname);
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


            CreateMap<ManagerContactCreateDTO, ManagerContact>()
                .ForMember(dest => dest.Translations, src => src.Ignore());

            CreateMap<ManagerContactEditDTO, ManagerContact>()
                .ForMember(dest => dest.Translations, src => src.Ignore());

        }
    }
}
