using SalesReach.Domain.Validations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.RegularExpressions;

namespace SalesReach.Domain.ValueObjects
{
    [Table("Endereco_Samuel")]
    public record Endereco : BaseValueObject
    {
        public int PessoaId { get; private set; }
        public string CEP { get; private set; }
        public string Logradouro { get; private set; }
        public string Numero { get; private set; }
        public string Complemento { get; private set; }
        public string Bairro { get; private set; }
        public string Localidade { get; private set; }
        public string UF { get; private set; }


        private Endereco(int pessoaId, string cep, string logradouro, string numero, string complemento, string bairro, string localidade, string uf, bool status, DateTime? dataAtualizacao, DateTime? dataCadastro)
            : base(status, dataAtualizacao, dataCadastro)
        {
            PessoaId = pessoaId;
            CEP = cep;
            Logradouro = logradouro;
            Numero = numero;
            Complemento = complemento;
            Bairro = bairro;
            Localidade = localidade;
            UF = uf;
        }

        public static Endereco Criar(int pessoaId, string cep, string logradouro, string numero, string complemento, string bairro, string localidade, string uf, bool status)
        {
            IsValidoEndereco(pessoaId, cep, logradouro, numero, complemento, bairro, localidade, uf);

            return new(pessoaId, ReplaceCEP(cep), logradouro, numero, complemento, bairro, localidade, uf, true, null, DateTime.Now);
        }

        public static Endereco Atualizar(Endereco enderecoAntigo, Endereco enderecoNovo)
        {
            IsValidoEndereco(enderecoNovo.PessoaId, enderecoNovo.CEP, enderecoNovo.Logradouro, enderecoNovo.Numero, enderecoNovo.Complemento, enderecoNovo.Bairro, enderecoNovo.Localidade, enderecoNovo.UF);

            if (enderecoAntigo.Equals(enderecoNovo))
                return enderecoAntigo;

            return enderecoAntigo with
            {
                CEP = ReplaceCEP(enderecoNovo.CEP),
                Logradouro = enderecoNovo.Logradouro,
                Numero = enderecoNovo.Numero,
                Complemento = enderecoNovo.Complemento,
                Bairro = enderecoNovo.Bairro,
                Localidade = enderecoNovo.Localidade,
                UF = enderecoNovo.UF,
                Status = true,
                DataAtualizacao = null,
                DataCadastro = DateTime.Now,
            };
        }

        public static Endereco Inativar(Endereco endereco)
        {
            endereco.Status = false;
            endereco.DataAtualizacao = DateTime.Now;
            return endereco;
        }

        private static void IsValidoEndereco(int pessoaId, string cep, string logradouro, string numero, string complemento, string bairro, string localidade, string uf)
        {
            DomainValidationException.When(pessoaId <= 0, "Pessoa Id é requerido.");
            DomainValidationException.When(Regex.IsMatch(cep, ("[0-9]{5}-[0-9]{3}")), "CEP informado é inválido.");
            DomainValidationException.When(logradouro is null, "Logradouro é requerido.");
            DomainValidationException.When(numero is null, "Número é requerido.");
            DomainValidationException.When(bairro is null, "Bairro é requerido.");
            DomainValidationException.When(localidade is null, "Localidade é requerido.");
            DomainValidationException.When(uf.Length != 2, "UF informado é inválido.");
        }

        private static string NormalizarCEP(string cep) => Convert.ToUInt64(cep).ToString(@"000000\-000");

        private static string ReplaceCEP(string cep) => cep.Replace("-", string.Empty);
    }
}
