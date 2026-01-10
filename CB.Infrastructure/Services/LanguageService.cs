
using CB.Application.Interfaces.Repositories;
using CB.Application.Interfaces.Services;
using CB.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace CB.Infrastructure.Services
{
    public class LanguageService : ILanguageService
    {
        private readonly IGenericRepository<Language> _repository;
        public LanguageService(IGenericRepository<Language> repository)
        {
            _repository = repository;
        }

        public async Task<List<string>> GetLanguageCodes()
        {
            List<string> codes = await _repository.GetQuery().Select(x => x.Code).ToListAsync();
            return codes;
        }
    }
}
