namespace SalesReach.Application.Models
{
    public class DocumentoModel
    {
        public int PessoaId { get; set; }
        public Guid Codigo { get; set; }
        public string DocumentoTipo { get; set; }
        public string NumeroDocumento { get; set; }
        public bool Status { get; set; }
        public DateTime? DataAtualizacao { get; set; }
        public DateTime DataCadastro { get; set; }
    }
}
