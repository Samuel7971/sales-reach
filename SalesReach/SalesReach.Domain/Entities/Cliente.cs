using SalesReach.Domain.Validations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SalesReach.Domain.Entities
{
    [Table("Cliente_Samuel")]
    public class Cliente : Pessoa
    {
        public int ClienteId { get; private set; }
        public bool Status { get; private set; }

        public Cliente() { } // Util para o Dapper
        private Cliente(int clienteId, int pessoaId, string nome, DateTime dataNascimento, DateTime? dataAtualizacao, DateTime? dataCadastro)
            : base(pessoaId, nome, dataNascimento, dataAtualizacao, dataCadastro)
        {
            ClienteId = clienteId;
            Status = true;
            DataAtualizacao = dataAtualizacao;
            DataCadastro = dataCadastro;
        }

        public static Cliente Criar(int clienteId, int pessoaId, string nome, DateTime dataNascimento)
        {
            Pessoa.EhValido(pessoaId);
            return new(clienteId, pessoaId, nome, dataNascimento, null, DateTime.Now);
        }

        public void Atualizar(int clienteId, int pessoaId, string nome, DateTime dataNascimento)
        {
            EhValido(clienteId);

            ClienteId = clienteId;
            DataAtualizacao = DateTime.Now;
            Atualizar(pessoaId, nome, dataNascimento);
        }

        public void AtualizarStatus(int clienteId, bool status) 
        {
            EhValido(clienteId);
            Status = status;
            DataAtualizacao = DateTime.Now;
        }

        private new void EhValido(int clienteId) => DomainValidationException.When(clienteId <= 0, "Obrigatório informar ClienteId.");

        public override bool Equals(object obj)
        {
            var compareTo = obj as Cliente;

            if (ReferenceEquals(this, compareTo)) return true;
            if (ReferenceEquals(null, compareTo)) return false;

            return ClienteId.Equals(compareTo.ClienteId);
        }

        public static bool operator ==(Cliente a, Cliente b)
        {
            if (ReferenceEquals(a, null) && ReferenceEquals(a, null)) return true;
            if (ReferenceEquals(a, null) || ReferenceEquals(b, null)) return false;

            return a.Equals(b);
        }

        public static bool operator !=(Cliente a, Cliente b) => !(a == b);

        public override int GetHashCode() => (GetType().GetHashCode() * 907) + ClienteId.GetHashCode();

    }
}
