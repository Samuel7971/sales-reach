using System.ComponentModel.DataAnnotations;

namespace SalesReach.Domain.Enums
{
    public enum EnumDocumentoTipo
    {
        [Display(Name = "CPF")]
        CPF = 1,
        [Display(Name = "CNPJ")]
        CNPJ = 2
    }
}
