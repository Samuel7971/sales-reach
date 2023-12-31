namespace SalesReach.Domain.Entities
{
    public abstract class Base
    {
        public int Id { get; set; }
        public Guid Codigo { get; set; } 
        public bool Status { get; set; }
        public DateTime? DataAtualizacao { get; set; }
        public DateTime? DataCadastro { get; set; }

        public Base() { } //Util para o Dapper
        public Base(int id, Guid codigo, bool status, DateTime? dataAtualizacao, DateTime? dataCadastro)
        {
            Id = id;
            Codigo = codigo;
            Status = status;
            DataAtualizacao = dataAtualizacao;
            DataCadastro = dataCadastro;
        }

        protected abstract void Validador();
        public abstract void AtualizarStatus(int Id, bool status);
    }
}
