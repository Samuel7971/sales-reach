using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesReach.Domain.ValueObjects
{
    public record class BaseValueObject
    {
        public Guid Codigo { get; set; }
        public bool Status { get; set; }
        public DateTime? DataAtualizacao { get; set; }
        public DateTime DataCadastro { get; set; }

        public override bool Equals(Documento documento)
        {
            if (documento.PessoaId.Equals(PessoaId) && documento.Codigo.Equals(Codigo) && documento.NumeroDocumento.Equals(NumeroDocumento))
                return true;

            return false;
        }
    }
}
