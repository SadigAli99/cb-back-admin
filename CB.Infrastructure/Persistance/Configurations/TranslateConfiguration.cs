
using CB.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CB.Infrastructure.Persistance.Configurations
{
    public class TranslateConfiguration : IEntityTypeConfiguration<Translate>
    {
        public void Configure(EntityTypeBuilder<Translate> builder)
        {
            var translate = new Translate { Id = 1, Key = "home", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow };
            builder.HasData(translate);
        }
    }

    public class TranslateTranslationConfiguration : IEntityTypeConfiguration<TranslateTranslation>
    {
        public void Configure(EntityTypeBuilder<TranslateTranslation> builder)
        {
            var translateValueAz = new TranslateTranslation { Id = 1, TranslateId = 1, Value = "Ana səhifə", LanguageId = 1, CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow };
            var translateValueEn = new TranslateTranslation { Id = 2, TranslateId = 1, Value = "Home", LanguageId = 2, CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow };
            builder.HasData(translateValueAz, translateValueEn);
        }
    }
}
