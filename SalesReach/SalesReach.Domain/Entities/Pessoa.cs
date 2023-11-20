using SalesReach.Domain.Validations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesReach.Domain.Entities
{
    [Table("Pessoa_Samuel")]
    public class Pessoa : Base
    {
        public string Nome { get; private set; }
        public int PessoaTipoId { get; private set; }
        public DateTime DataNascimento { get; private set; }

        public Pessoa() { }

        public Pessoa(int id, string nome, int pessoaTipoId, DateTime dataNascimento, bool status, DateTime dataAtualizacao, DateTime dataCadastro)
        {
            Id = id;
            Nome = nome;
            PessoaTipoId = pessoaTipoId;
            DataNascimento = dataNascimento;
            Status = status;    
            DataAtualizacao = dataAtualizacao;
            DataCadastro = dataCadastro;
        }

        public void IsValidaPessoa(string nome, int pessoaTipoId, DateTime dataNascimento)
        {
            DomainValidationException.When(string.IsNullOrEmpty(nome), "Obrigatorio informar campo Nome.");
            DomainValidationException.When(!IsValidaDataNascimento(dataNascimento), "Data Nascimento informada é inválida.");
            DomainValidationException.When(pessoaTipoId < 0, "É preciso informar se o cadastro é de Pessoa Fisíca ou Juridica.");
        }

        public void Inserir(string nome, int pessoaTipoId, DateTime dataNascimento, bool status)
        {
            IsValidaPessoa(nome, pessoaTipoId, dataNascimento);

            Nome = nome;
            PessoaTipoId = pessoaTipoId;
            DataNascimento = dataNascimento;
            Status = status;
            DataCadastro = DateTime.Now;
        }

        public void Atualizar(int id, string nome, int pessoaTipoId, DateTime dataNascimento, bool status)
        {
            IsValidaPessoa(nome, pessoaTipoId, dataNascimento);

            Id = id;
            Nome = nome;
            PessoaTipoId = pessoaTipoId;
            DataNascimento = dataNascimento;
            Status = status;
        }

        private bool IsValidaDataNascimento(DateTime dataNascimento)
            => dataNascimento <= DateTime.Now.AddYears(-16);
    }
}
