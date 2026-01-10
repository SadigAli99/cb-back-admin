using AutoMapper;
using CB.Application.DTOs.Address;
using CB.Application.Interfaces.Repositories;
using CB.Application.Interfaces.Services;
using CB.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace CB.Infrastructure.Services
{
    public class AddressService : IAddressService
    {
        private readonly IGenericRepository<Address> _repository;
        private readonly IGenericRepository<Language> _languageRepository;
        private readonly IMapper _mapper;

        public AddressService(
            IGenericRepository<Address> repository,
            IGenericRepository<Language> languageRepository,
            IMapper mapper
        )
        {
            _repository = repository;
            _languageRepository = languageRepository;
            _mapper = mapper;
        }

        public async Task<List<AddressGetDTO>> GetAllAsync()
        {
            var entities = await _repository.GetQuery()
                .Include(t => t.Translations)
                .ThenInclude(tt => tt.Language)
                .ToListAsync();

            return _mapper.Map<List<AddressGetDTO>>(entities);
        }

        public async Task<AddressGetDTO?> GetByIdAsync(int id)
        {
            var entity = await _repository.GetQuery()
                .Include(t => t.Translations)
                .ThenInclude(tt => tt.Language)
                .FirstOrDefaultAsync(t => t.Id == id);

            return entity == null ? null : _mapper.Map<AddressGetDTO>(entity);
        }

        public async Task<bool> CreateAsync(AddressCreateDTO dto)
        {
            var languages = await _languageRepository.GetAllAsync();

            var entity = new Address
            {
                Map = dto.Map,
                IsMain = dto.IsMain,
                Translations = dto.Titles.Select(v =>
                {
                    var lang = languages.FirstOrDefault(l => l.Code == v.Key);
                    if (lang == null)
                        throw new Exception($"'{v.Key}' kodu ilə dil tapılmadı.");

                    dto.Texts.TryGetValue(v.Key, out var text);

                    return new AddressTranslation
                    {
                        LanguageId = lang.Id,
                        Title = v.Value,
                        Text = text ?? string.Empty
                    };
                }).ToList()
            };

            return await _repository.AddAsync(entity);
        }

        public async Task<bool> UpdateAsync(int id, AddressEditDTO dto)
        {
            var entity = await _repository.GetQuery()
                .Include(t => t.Translations)
                .FirstOrDefaultAsync(t => t.Id == id);

            if (entity == null) return false;

            _mapper.Map(dto, entity);

            var languages = await _languageRepository.GetQuery().ToListAsync();

            entity.Translations = dto.Titles.Select(v =>
            {
                var lang = languages.FirstOrDefault(l => l.Code == v.Key);
                if (lang == null)
                    throw new Exception($"'{v.Key}' kodu ilə dil tapılmadı.");

                dto.Texts.TryGetValue(v.Key, out var text);

                return new AddressTranslation
                {
                    LanguageId = lang.Id,
                    Title = v.Value,
                    Text = text ?? string.Empty
                };
            }).ToList();

            return await _repository.UpdateAsync(entity);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _repository.GetQuery()
                .Include(t => t.Translations)
                .FirstOrDefaultAsync(t => t.Id == id);

            if (entity == null) return false;

            return await _repository.DeleteAsync(entity);
        }
    }
}
