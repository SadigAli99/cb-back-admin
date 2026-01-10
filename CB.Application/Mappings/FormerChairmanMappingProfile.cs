

using CB.Application.DTOs.FormerChairman;
using CB.Core.Entities;

namespace CB.Application.Mappings
{
    public class FormerChairmanMappingProfile : MappingProfile
    {
        public FormerChairmanMappingProfile() : base()
        {
            CreateMap<FormerChairman, FormerChairmanGetDTO>()
                .ForMember(dest => dest.Fullnames, opt =>
                {
                    opt.MapFrom(src => src.Translations.ToDictionary(
                        t => t.Language.Code,
                        t => t.Fullname
                    ));
                })
                .ForMember(dest => dest.Descriptions, opt =>
                {
                    opt.MapFrom(src => src.Translations.ToDictionary(
                        t => t.Language.Code,
                        t => t.Description
                    ));
                });


            CreateMap<FormerChairmanCreateDTO, FormerChairman>()
                .ForMember(dest => dest.Translations, src => src.Ignore());

            CreateMap<FormerChairmanEditDTO, FormerChairman>()
                .ForMember(dest => dest.Translations, src => src.Ignore());

        }
    }
}
