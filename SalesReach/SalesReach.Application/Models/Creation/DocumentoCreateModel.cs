using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesReach.Application.Models.Creation
{
    public class DocumentoCreateModel
    {
        public int PessoaId { get; set; }
        public Guid Codigo { get; set; }
        public string NumeroDocumento { get; set; }
    }
}
