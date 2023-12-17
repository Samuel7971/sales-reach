namespace SalesReach.Application.Models
{
    public class EnderecoModel
    {
        public int PessoaId { get; set; }
        public Guid Codigo { get; set; }
        public string CEP { get; set; }
        public string Logradouro { get; set; }
        public string Numero { get; set; }
        public string Complemento { get; set; }
        public string Bairro { get; set; }
        public string Localidade { get; set; }
        public string UF { get; set; }
        public bool Status { get; set; }
    }
}
