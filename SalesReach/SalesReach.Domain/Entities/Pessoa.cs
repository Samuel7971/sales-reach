using SalesReach.Domain.Enums;
using SalesReach.Domain.Validations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SalesReach.Domain.Entities
{
    [Table("Pessoa_Samuel")]
    public class Pessoa : Base
    {
        public string Nome { get; private set; }
        public PessoaTipo PessoaTipo { get; private set; }
        public DateTime DataNascimento { get; private set; }

        public Pessoa() { }

        public Pessoa(int id, string nome, PessoaTipo pessoaTipo, DateTime dataNascimento, bool status, DateTime dataAtualizacao, DateTime dataCadastro)
        {
            Id = id;
            Nome = nome;
            PessoaTipo = pessoaTipo;
            DataNascimento = dataNascimento;
            Status = status;    
            DataAtualizacao = dataAtualizacao;
            DataCadastro = dataCadastro;
        }

        public void IsValidaPessoa(string nome, PessoaTipo pessoaTipo, DateTime dataNascimento)
        {
            DomainValidationException.When(string.IsNullOrEmpty(nome), "Obrigatorio informar campo Nome.");
            DomainValidationException.When(!IsValidaDataNascimento(dataNascimento), "Data Nascimento informada é inválida.");
            DomainValidationException.When(pessoaTipo < 0, "É preciso informar se o cadastro é de Pessoa Fisíca ou Juridica.");
        }

        public void Inserir(string nome, PessoaTipo pessoaTipo, DateTime dataNascimento, bool status)
        {
            IsValidaPessoa(nome, pessoaTipo, dataNascimento);

            Nome = nome;
            PessoaTipo = pessoaTipo;
            DataNascimento = dataNascimento;
            Status = status;
            DataCadastro = DateTime.Now;
        }

        public void Atualizar(int id, string nome, PessoaTipo pessoaTipo, DateTime dataNascimento, bool status)
        {
            IsValidaPessoa(nome, pessoaTipo, dataNascimento);

            Id = id;
            Nome = nome;
            PessoaTipo = pessoaTipo;
            DataNascimento = dataNascimento;
            Status = status;
        }

        public static implicit operator string(Pessoa pessoa)
            => $@"{pessoa.Id}, {pessoa.Nome}, {pessoa.Status}, {pessoa.DataNascimento}, {pessoa.DataAtualizacao}, {pessoa.DataCadastro}";

        public static implicit operator Pessoa(string input)
        {
            var data = input.Split(',');
            return new Pessoa(
                                 id: int.Parse(data[0]), 
                                 nome: data[1], 
                                 pessoaTipo: Enum.Parse<PessoaTipo>(data[2]), 
                                 dataNascimento: DateTime.Parse(data[3]), 
                                 status: bool.Parse(data[4]), 
                                 dataAtualizacao: DateTime.Parse(data[5]), 
                                 dataCadastro: DateTime.Parse(data[6])
                              );
        }

        private bool IsValidaDataNascimento(DateTime dataNascimento)
            => dataNascimento <= DateTime.Now.AddYears(-16);
    }
}
