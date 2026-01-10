using System.ComponentModel.DataAnnotations;

namespace CB.Core.Enums
{
    public enum DirectorType
    {
        [Display(Name = "Sədr")]
        CHAIRMAN = 1,
        [Display(Name = "Sədr müavini")]
        VICECHAIRMAN = 2
    }
}
