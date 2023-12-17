using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesReach.Domain.Enums
{
    public enum DocumentoTipo
    {
        [Display(Name = "CPF")]
        CPF = 1,
        [Display(Name = "CNPJ")]
        CNPJ = 2,
        [Display(Name = "RG")]
        RG = 3
    }
}
