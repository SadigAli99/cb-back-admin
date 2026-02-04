using AutoMapper;
using CB.Application.DTOs.Contact;
using CB.Application.Interfaces.Repositories;
using CB.Application.Interfaces.Services;
using CB.Core.Entities;
using CB.Shared.Extensions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;

namespace CB.Infrastructure.Services
{
    public class ContactService : IContactService
    {
        private readonly IGenericRepository<Contact> _repository;
        private readonly IGenericRepository<Language> _languageRepository;
        private readonly IWebHostEnvironment _env;
        private readonly IMapper _mapper;

        public ContactService(
            IMapper mapper,
            IGenericRepository<Contact> repository,
            IGenericRepository<Language> languageRepository,
            IWebHostEnvironment env
        )
        {
            _mapper = mapper;
            _repository = repository;
            _languageRepository = languageRepository;
            _env = env;
        }

        public async Task<bool> CreateOrUpdate(ContactPostDTO dto)
        {
            var languages = await _languageRepository.GetAllAsync();
            Contact? contact = await _repository.GetQuery()
                .Include(h => h.Translations)
                .ThenInclude(x => x.Language)
                .FirstOrDefaultAsync();

            bool result;

            if (contact is null)
            {
                contact = _mapper.Map<Contact>(dto);
                if (dto.File != null)
                {
                    contact.ReceptionSchedule = await dto.File.FileUpload(_env.WebRootPath, "contacts");
                    float fileSize = (float)Math.Round(dto.File.Length / 1024f, 2);
                    contact.FileSize = $"{fileSize} KB";
                }

                contact.Translations = dto.Notes.Select(v =>
                {
                    var lang = languages.FirstOrDefault(l => l.Code == v.Key);
                    if (lang == null)
                        throw new Exception($"'{v.Key}' kodu ilə dil tapılmadı.");

                    dto.RegistrationTimes.TryGetValue(v.Key, out var registrationTime);

                    return new ContactTranslation
                    {
                        LanguageId = lang.Id,
                        Note = v.Value,
                        RegistrationTime = registrationTime
                    };
                }).ToList();

                result = await _repository.AddAsync(contact);
            }
            else
            {
                _mapper.Map(dto, contact);
                if (dto.File != null)
                {
                    FileManager.FileDelete(_env.WebRootPath, contact.ReceptionSchedule ?? "");
                    contact.ReceptionSchedule = await dto.File.FileUpload(_env.WebRootPath, "contacts");
                    float fileSize = (float)Math.Round(dto.File.Length / 1024f, 2);
                    contact.FileSize = $"{fileSize} KB";
                }

                foreach (var v in dto.Notes)
                {
                    var lang = languages.FirstOrDefault(l => l.Code == v.Key);
                    if (lang == null)
                        throw new Exception($"'{v.Key}' kodu ilə dil tapılmadı.");

                    dto.RegistrationTimes.TryGetValue(v.Key, out var registrationTime);


                    var existingTranslation = contact.Translations.FirstOrDefault(t => t.LanguageId == lang.Id);

                    if (existingTranslation != null)
                    {
                        existingTranslation.Note = v.Value;
                        existingTranslation.RegistrationTime = registrationTime;
                    }
                    else
                    {
                        contact.Translations.Add(new ContactTranslation
                        {
                            LanguageId = lang.Id,
                            Note = v.Value,
                            RegistrationTime = registrationTime
                        });
                    }
                }

                result = await _repository.UpdateAsync(contact);
            }


            return result;
        }

        public async Task<ContactGetDTO?> GetFirst()
        {
            Contact? contact = await _repository.GetQuery()
                .Include(h => h.Translations)
                .ThenInclude(x => x.Language)
                .FirstOrDefaultAsync(h => h.Id == 1);

            return contact == null ? null : _mapper.Map<ContactGetDTO>(contact);
        }
    }
}
