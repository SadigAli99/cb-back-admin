

using CB.Application.DTOs.CorrespondentAccount;
using CB.Application.Mappings.Resolvers;
using CB.Core.Entities;

namespace CB.Application.Mappings
{
    public class CorrespondentAccountMappingProfile : MappingProfile
    {
        public CorrespondentAccountMappingProfile() : base()
        {
            CreateMap<CorrespondentAccount, CorrespondentAccountGetDTO>()
                .ForMember(dest => dest.File, opt => opt.MapFrom<GenericResolver<CorrespondentAccount, CorrespondentAccountGetDTO>>())
                .ForMember(dest => dest.Titles, opt =>
                {
                    opt.MapFrom(src => src.Translations.ToDictionary(
                        t => t.Language.Code,
                        t => t.Title
                    ));
                })
                .ForMember(dest => dest.CoverTitles, opt =>
                {
                    opt.MapFrom(src => src.Translations.ToDictionary(
                        t => t.Language.Code,
                        t => t.CoverTitle
                    ));
                });


            CreateMap<CorrespondentAccountCreateDTO, CorrespondentAccount>()
                .ForMember(dest => dest.Translations, src => src.Ignore())
                .ForMember(dest => dest.FileType, src => src.Ignore())
                .ForMember(dest => dest.File, src => src.Ignore());

            CreateMap<CorrespondentAccountEditDTO, CorrespondentAccount>()
                .ForMember(dest => dest.Translations, src => src.Ignore())
                .ForMember(dest => dest.FileType, src => src.Ignore())
                .ForMember(dest => dest.File, src => src.Ignore());

        }
    }
}
