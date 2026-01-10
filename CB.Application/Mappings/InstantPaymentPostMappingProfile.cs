

using CB.Application.DTOs.InstantPaymentPost;
using CB.Application.Mappings.Resolvers;
using CB.Core.Entities;

namespace CB.Application.Mappings
{
    public class InstantPaymentPostMappingProfile : MappingProfile
    {
        public InstantPaymentPostMappingProfile() : base()
        {
            CreateMap<InstantPaymentPost, InstantPaymentPostGetDTO>()
                .ForMember(dest => dest.Titles, opt =>
                {
                    opt.MapFrom(src => src.Translations.ToDictionary(
                        t => t.Language.Code,
                        t => t.Title
                    ));
                })
                .ForMember(dest => dest.Slugs, opt =>
                {
                    opt.MapFrom(src => src.Translations.ToDictionary(
                        t => t.Language.Code,
                        t => t.Slug
                    ));
                })
                .ForMember(dest => dest.ShortDescriptions, opt =>
                {
                    opt.MapFrom(src => src.Translations.ToDictionary(
                        t => t.Language.Code,
                        t => t.ShortDescription
                    ));
                })
                .ForMember(dest => dest.Descriptions, opt =>
                {
                    opt.MapFrom(src => src.Translations.ToDictionary(
                        t => t.Language.Code,
                        t => t.Description
                    ));
                });


            CreateMap<InstantPaymentPostCreateDTO, InstantPaymentPost>()
                .ForMember(dest => dest.Translations, src => src.Ignore());

            CreateMap<InstantPaymentPostEditDTO, InstantPaymentPost>()
                .ForMember(dest => dest.Translations, src => src.Ignore());

        }
    }
}
