

using CB.Application.DTOs.InternshipDirection;
using CB.Application.Mappings.Resolvers;
using CB.Core.Entities;

namespace CB.Application.Mappings
{
    public class InternshipDirectionMappingProfile : MappingProfile
    {
        public InternshipDirectionMappingProfile() : base()
        {
            CreateMap<InternshipDirection, InternshipDirectionGetDTO>()
                .ForMember(dest => dest.Icon, src => src.MapFrom<GenericResolver<InternshipDirection, InternshipDirectionGetDTO>>())
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


            CreateMap<InternshipDirectionCreateDTO, InternshipDirection>()
                .ForMember(dest => dest.Translations, src => src.Ignore());

            CreateMap<InternshipDirectionEditDTO, InternshipDirection>()
                .ForMember(dest => dest.Translations, src => src.Ignore());

        }
    }
}
