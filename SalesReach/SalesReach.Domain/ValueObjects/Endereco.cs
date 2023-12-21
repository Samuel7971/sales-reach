using SalesReach.Domain.Validations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.RegularExpressions;

namespace SalesReach.Domain.ValueObjects
{
    [Table("Endereco_Samuel")]
    public record class Endereco : BaseValueObject
    {
        public int PessoaId { get; private set; }
        public string CEP { get; private set; }
        public string Logradouro { get; private set; }
        public string Numero { get; private set; }
        public string Complemento { get; private set; }
        public string Bairro { get; private set; }
        public string Localidade { get; private set; }
        public string UF { get; private set; }

        public Endereco() { }

        public Endereco(int pessoaId, Guid codigo, string cep, string logradouro, string numero, string complemento, string bairro, string localidade, string uf, bool status)
        {
            PessoaId = pessoaId;
            Codigo = codigo;
            CEP = cep;
            Logradouro = logradouro;
            Numero = numero;
            Complemento = complemento;
            Bairro = bairro;
            Localidade = localidade;
            UF = uf;
            Status = status;
        }

        private void IsValidoEndereco(int pessoaId, Guid codigo, string cep, string logradouro, string numero, string complemento, string bairro, string localidade, string uf)
        {
            DomainValidationException.When(pessoaId <= 0, "Pessoa Id é requerido.");
            DomainValidationException.When(codigo == Guid.Empty, "Código é requerido.");
            DomainValidationException.When(Regex.IsMatch(cep, ("[0-9]{5}-[0-9]{3}")), "CEP informado é inválido.");
            DomainValidationException.When(logradouro is null, "Logradouro não pode ser nulo.");
            DomainValidationException.When(numero is null, "Número não pode ser nulo.");
            DomainValidationException.When(bairro is null, "Bairro não pode ser nulo.");
            DomainValidationException.When(uf.Length != 2, "UF informado é inválido.");
        }

        public void Inserir(int pessoaId, Guid codigo, string cep, string logradouro, string numero, string complemento, string bairro, string localidade, string uf, bool status)
        {
            IsValidoEndereco(pessoaId, codigo, cep, logradouro, numero, complemento, bairro,localidade, uf);

            PessoaId = pessoaId;
            Codigo = codigo;
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
            IsValidoEndereco(endereco.PessoaId, endereco.Codigo, cep, logradouro, numero, complemento, bairro, localidade, uf);

            return endereco with { 
                                    Codigo = Codigo,
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
