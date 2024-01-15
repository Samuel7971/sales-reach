namespace SalesReach.Domain.ValueObjects
{
    public record BaseValueObject
    {
        public bool Status { get; set; }
        public DateTime? DataAtualizacao { get; set; }
        public DateTime? DataCadastro { get; set; }

        public BaseValueObject(bool status, DateTime? dataAtualizacao, DateTime? dataCadastro)
        {
            Status = status;
            DataAtualizacao = dataAtualizacao;
            DataCadastro = dataCadastro;
        }
    }
}
