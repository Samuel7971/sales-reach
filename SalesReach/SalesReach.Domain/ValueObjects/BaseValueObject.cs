namespace SalesReach.Domain.ValueObjects
{
    public record class BaseValueObject
    {
        public int PessoaId { get; private set; }
        public Guid Codigo { get; private set; }
        public bool Status { get; private set; }
        public DateTime? DataAtualizacao { get; private set; }
        public DateTime? DataCadastro { get; private set; }

        public BaseValueObject() { }

        public BaseValueObject(int pessoaId, Guid codigo, bool status, DateTime? dataAtualizacao, DateTime? dataCadastro)
        {
            PessoaId = pessoaId;
            Codigo = codigo;
            Status = status;
            DataAtualizacao = dataAtualizacao;
            DataCadastro = dataCadastro;
        }

        public virtual void AtualizarStatus(int pessoaId, bool status, DateTime dataAtualizacao)
        {
            PessoaId = pessoaId;
            Status = status;
            DataAtualizacao = dataAtualizacao;
        }

        public virtual bool Equals(BaseValueObject outro) 
            => outro is not null && PessoaId == outro.PessoaId && Codigo == outro.Codigo;

        public override int GetHashCode() => base.GetHashCode();
    }
}
