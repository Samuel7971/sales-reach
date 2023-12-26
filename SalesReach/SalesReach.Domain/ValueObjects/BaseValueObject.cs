namespace SalesReach.Domain.ValueObjects
{
    public record class BaseValueObject
    {
        public Guid Codigo { get; set; }
        public bool Status { get; set; }
        public DateTime? DataAtualizacao { get; set; }
        public DateTime DataCadastro { get; set; }
    }
}
