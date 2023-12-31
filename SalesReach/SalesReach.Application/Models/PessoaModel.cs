using SalesReach.Domain.Enums;

namespace SalesReach.Application.Models
{
    public class PessoaModel
    {
        public int Id { get; set; }
        public Guid Codigo { get; private set; }
        public string Nome { get; private set; }
        public DateTime DataNascimento { get; private set; }
        public bool Status { get; set; }
        public DateTime? DataAtualizacao { get; set; }
        public DateTime? DataCadastro { get; set; }
    }
}
