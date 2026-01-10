
using System.ComponentModel.DataAnnotations;

namespace CB.Core.Enums
{
    public enum VacancyType
    {
        [Display(Name = "Tam iş günü")]
        FULLTIME = 1,
        [Display(Name = "Yarım iş günü")]
        PARTTIME = 2,
        [Display(Name = "Uzaqdan")]
        REMOTE = 3,
        [Display(Name = "Hibrid")]
        HYBRID = 4,
        [Display(Name = "Frilans")]
        FREELANCE = 5,
    }
}
