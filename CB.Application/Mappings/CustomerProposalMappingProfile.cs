
using CB.Application.DTOs.CustomerProposal;
using CB.Core.Entities;

namespace CB.Application.Mappings
{
    public class CustomerProposalMappingProfile : MappingProfile
    {
        public CustomerProposalMappingProfile()
        {
            CreateMap<CustomerProposal, CustomerProposalGetDTO>()
                .ForMember(dest => dest.Descriptions, opt =>
                    opt.MapFrom(src => src.Translations
                        .ToDictionary(
                            t => t.Language.Code,
                            t => t.Description
                        )
                    )
                );

            CreateMap<CustomerProposalPostDTO, CustomerProposal>()
                .ForMember(dest => dest.Translations, opt => opt.Ignore())
                .ReverseMap();
        }
    }
}
