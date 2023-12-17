using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesReach.Application.Models.Creation
{
    public class PessoaCreateModel
    {
        public string Nome { get; set; }
        public DateTime DataNascimento { get; set; }
        public DocumentoCreateModel Documento { get; set; }
        public EnderecoCreateModel Endereco { get; set; }
    }
}
