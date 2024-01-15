namespace SalesReach.Domain.ValueObjects
{
    public record BaseValueObject
    {
        public DateTime? DataAtualizacao { get; set; }
        public DateTime? DataCadastro { get; set; }

        public BaseValueObject(DateTime? dataAtualizacao, DateTime? dataCadastro)
        {
            DataAtualizacao = dataAtualizacao;
            DataCadastro = dataCadastro;
        }
    }
}
