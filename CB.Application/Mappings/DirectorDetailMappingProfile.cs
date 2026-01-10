

using CB.Application.DTOs.DirectorDetail;
using CB.Application.Mappings.Resolvers;
using CB.Core.Entities;

namespace CB.Application.Mappings
{
    public class DirectorDetailMappingProfile : MappingProfile
    {
        public DirectorDetailMappingProfile() : base()
        {
            CreateMap<DirectorDetail, DirectorDetailGetDTO>()
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


            CreateMap<DirectorDetailCreateDTO, DirectorDetail>()
                .ForMember(dest => dest.Translations, src => src.Ignore());

            CreateMap<DirectorDetailEditDTO, DirectorDetail>()
                .ForMember(dest => dest.Translations, src => src.Ignore());

        }
    }
}
