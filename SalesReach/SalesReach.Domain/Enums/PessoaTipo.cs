using System.ComponentModel.DataAnnotations;

namespace SalesReach.Domain.Enums
{
    public enum PessoaTipo
    {
        [Display(Name = "Fisíca")]
        Fisica = 1,
        [Display(Name = "Juridica")]
        Juridica = 2
    }
}
