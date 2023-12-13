using SalesReach.Domain.Validations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.RegularExpressions;

namespace SalesReach.Domain.ValueObjects
{
    [Table("Endereco_Samuel")]
    public record class Endereco
    {
        public int PessoaId { get; private set; }
        public string CEP { get; private set; }
        public string Logradouro { get; private set; }
        public string Numero { get; private set; }
        public string Complemento { get; private set; }
        public string Bairro { get; private set; }
        public string Localidade { get; private set; }
        public string UF { get; private set; }
        public bool Status { get; private set; }
        public DateTime? DataAtualizacao { get; private set; }
        public DateTime DataCadastro { get; private set; }

        public Endereco() { }

        public Endereco(int pessoaId, string cep, string logradouro, string numero, string complemento, string bairro, string localidade, string uf, bool status)
        {
            PessoaId = pessoaId;
            CEP = cep;
            Logradouro = logradouro;
            Numero = numero;
            Complemento = complemento;
            Bairro = bairro;
            Localidade = localidade;
            UF = uf;
            Status = status;
        }

        private void IsValidoEndereco(int pessoaId, string cep, string logradouro, string numero, string complemento, string bairro, string localidade, string uf)
        {
            DomainValidationException.When(pessoaId <= 0, "PessoaId é requerido.");
            DomainValidationException.When(Regex.IsMatch(cep, ("[0-9]{5}-[0-9]{3}")), "CEP informado é inválido.");
            DomainValidationException.When(logradouro is null, "Logradouro não pode ser nulo.");
            DomainValidationException.When(numero is null, "Número não pode ser nulo.");
            DomainValidationException.When(bairro is null, "Bairro não pode ser nulo.");
            DomainValidationException.When(uf.Length != 2, "UF informado é inválido.");
        }

        public void Inserir(int pessoaId, string cep, string logradouro, string numero, string complemento, string bairro, string localidade, string uf, bool status)
        {
            IsValidoEndereco(pessoaId, cep, logradouro, numero, complemento, bairro,localidade, uf);

            PessoaId = pessoaId;
            CEP = ReplaceCEP(cep);
            Logradouro = logradouro;
            Numero = numero;
            Complemento = complemento;
            Bairro = bairro;
            Localidade = localidade;
            UF = uf;
            Status = true;
            DataAtualizacao = null;
            DataCadastro = DateTime.Now;
        }

        public Endereco Atualizar(Endereco endereco, string cep, string logradouro, string numero, string complemento, string bairro, string localidade, string uf, bool status)
        {
            IsValidoEndereco(endereco.PessoaId, cep, logradouro, numero, complemento, bairro, localidade, uf);

            return endereco with { 
                                    CEP = cep,  
                                    Logradouro = logradouro, 
                                    Numero = numero, 
                                    Complemento = complemento, 
                                    Bairro = bairro, 
                                    Localidade = localidade, 
                                    UF = uf, 
                                    Status = true,
                                    DataAtualizacao = DateTime.Now
                                 };
        }

        public Endereco Inativar(Endereco endereco)
        {
            endereco.Status = false;
            endereco.DataAtualizacao = DateTime.Now;
            return endereco;
        }

        private static string NormalizarCEP(string cep) => Convert.ToUInt64(cep).ToString(@"000000\-000");

        private static string ReplaceCEP(string cep) => cep.Replace("-", string.Empty);
    }
}
