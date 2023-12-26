using SalesReach.Domain.Validations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SalesReach.Domain.Entities
{
    [Table("Cliente_Samuel")]
    public class Cliente : Base
    {
        public Pessoa Pessoa { get; private set; }

        public Cliente() { }

        protected override void Validador()
        {
            throw new NotImplementedException();
        }
        private void IsValidoCliente(Pessoa pessoa)
        {
            DomainValidationException.When(pessoa is null, "Pessoa Id informado é inválido.");
        }

        public void Inserir(Pessoa pessoa)
        {
            IsValidoCliente(pessoa);

            Status = true;
            DataAtualizacao = null;
            DataCadastro = DateTime.Now;
        }

        public override void AtualizarStatus(int id, bool status, DateTime dataAtualizacao )
        {
            DomainValidationException.When(id <= 0, "Id informado é inválido.");
            
            Status = status;
            DataAtualizacao = DateTime.Now;
        }
    }
}
