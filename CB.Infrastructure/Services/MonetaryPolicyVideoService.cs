using AutoMapper;
using CB.Application.DTOs.MonetaryPolicyVideo;
using CB.Application.Interfaces.Repositories;
using CB.Application.Interfaces.Services;
using CB.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace CB.Infrastructure.Services
{
    public class MonetaryPolicyVideoService : IMonetaryPolicyVideoService
    {
        private readonly IGenericRepository<MonetaryPolicyVideo> _repository;
        private readonly IMapper _mapper;

        public MonetaryPolicyVideoService(
            IMapper mapper,
            IGenericRepository<MonetaryPolicyVideo> repository
        )
        {
            _mapper = mapper;
            _repository = repository;
        }

        public async Task<bool> CreateOrUpdate(MonetaryPolicyVideoPostDTO dto)
        {;
            MonetaryPolicyVideo? monetaryPolicyVideo = await _repository.GetQuery().FirstOrDefaultAsync();

            bool result;

            if (monetaryPolicyVideo is null)
            {
                monetaryPolicyVideo = _mapper.Map<MonetaryPolicyVideo>(dto);
                result = await _repository.AddAsync(monetaryPolicyVideo);
            }
            else
            {
                _mapper.Map(dto, monetaryPolicyVideo);
                result = await _repository.UpdateAsync(monetaryPolicyVideo);
            }


            return result;
        }

        public async Task<MonetaryPolicyVideoGetDTO?> GetFirst()
        {
            MonetaryPolicyVideo? monetaryPolicyVideo = await _repository.GetQuery().FirstOrDefaultAsync();

            return monetaryPolicyVideo == null ? null : _mapper.Map<MonetaryPolicyVideoGetDTO>(monetaryPolicyVideo);
        }
    }
}
