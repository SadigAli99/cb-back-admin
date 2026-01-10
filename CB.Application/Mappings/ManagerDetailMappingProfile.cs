

using CB.Application.DTOs.ManagerDetail;
using CB.Core.Entities;

namespace CB.Application.Mappings
{
    public class ManagerDetailMappingProfile : MappingProfile
    {
        public ManagerDetailMappingProfile() : base()
        {
            CreateMap<ManagerDetail, ManagerDetailGetDTO>()
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


            CreateMap<ManagerDetailCreateDTO, ManagerDetail>()
                .ForMember(dest => dest.Translations, src => src.Ignore());

            CreateMap<ManagerDetailEditDTO, ManagerDetail>()
                .ForMember(dest => dest.Translations, src => src.Ignore());

        }
    }
}
