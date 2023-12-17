using SalesReach.Domain.Enums;
using SalesReach.Domain.Enums.Extensions;
using SalesReach.Domain.Validations;
using SalesReach.Domain.ValueObjects;
using System.ComponentModel.DataAnnotations.Schema;

namespace SalesReach.Domain.Entities
{
    [Table("Pessoa_Samuel")]
    public class Pessoa : Base
    {
        public Guid Codigo { get; private set; } 
        public string Nome { get; private set; }
        public DateTime DataNascimento { get; private set; }
        public Documento Documento{ get; private set; }
        public Endereco Endreco { get; private set; }

        public Pessoa() { }

        public Pessoa(string nome, DateTime dataNascimento)
        {
            Nome = nome;
            DataNascimento = dataNascimento;
        }

        public Pessoa(int id, string nome, DateTime dataNascimento, bool status, DateTime dataAtualizacao, DateTime dataCadastro)
        {
            Id = id;
            Nome = nome;
            PessoaTipo = pessoaTipo;
            DataNascimento = dataNascimento;
            Status = status;    
            DataAtualizacao = dataAtualizacao;
            DataCadastro = dataCadastro;
        }

        private void IsValidaPessoa(string nome, DateTime dataNascimento)
        {
            DomainValidationException.When(string.IsNullOrEmpty(nome), "Obrigatorio informar campo Nome.");
            DomainValidationException.When(!IsValidaDataNascimento(dataNascimento), "Data Nascimento informada é inválida.");
        }

        public void Inserir(string nome, DateTime dataNascimento)
        {
            IsValidaPessoa(nome, dataNascimento);

            Codigo = GerarCodigoPessoa();
            Nome = nome;
            DataNascimento = dataNascimento;
            Status = true;
            DataCadastro = DateTime.Now;
        }

        public void Atualizar(int id, Guid codigo, string nome, DateTime dataNascimento, bool status)
        {
            IsValidaPessoa(nome, dataNascimento);

            Id = id;
            Codigo = codigo;
            Nome = nome;
            
            DataNascimento = dataNascimento;
            Status = status;
            DataAtualizacao = DateTime.Now;
        }

        public static implicit operator string(Pessoa pessoa)
            => $@"{pessoa.Id}, {pessoa.Nome}, {pessoa.DataNascimento}, {pessoa.Status}, {pessoa.DataAtualizacao}, {pessoa.DataCadastro}";

        public static implicit operator Pessoa(string input)
        {
            var data = input.Split(',');
            return new Pessoa(
                                 id: int.Parse(data[0]), 
                                 nome: data[1], 
                                 dataNascimento: DateTime.Parse(data[2]), 
                                 status: bool.Parse(data[3]), 
                                 dataAtualizacao: data[4] != null ? DateTime.Parse(data[4]) : DateTime.Parse(data[5]), 
                                 dataCadastro: DateTime.Parse(data[5])
                              );
        }

        private bool IsValidaDataNascimento(DateTime dataNascimento)
            => dataNascimento <= DateTime.Now.AddYears(-16);

        private Guid GerarCodigoPessoa() => Guid.NewGuid();

        
        
    }
}
