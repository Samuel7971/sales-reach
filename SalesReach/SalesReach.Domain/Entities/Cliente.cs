using SalesReach.Domain.Validations;

namespace SalesReach.Domain.Entities
{
    public class Cliente : Base
    {
        public Pessoa Pessoa { get; private set; }

        public Cliente() { }

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

        public void AtualizarStatus(int id, bool status)
        {
            DomainValidationException.When(id <= 0, "Id informado é inválido.");
            
            Status = status;
            DataAtualizacao = DateTime.Now;
        }
    }
}
