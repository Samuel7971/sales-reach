namespace SalesReach.Application.Models.Creation
{
    public class PessoaCreateModel
    {
        public string Nome { get; set; }
        public DateTime DataNascimento { get; set; }
        public DocumentoCreateModel Documento { get; set; }
        public EnderecoCreateModel Endereco { get; set; }
    }
}
