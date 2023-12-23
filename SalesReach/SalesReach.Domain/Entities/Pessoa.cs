using SalesReach.Domain.Interface;
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

            IsValid();
        }

        public Pessoa(int id, Guid codigo,  string nome, DateTime dataNascimento, bool status, DateTime? dataAtualizacao, DateTime dataCadastro)
        {
            Id = id;
            Codigo = codigo;
            Nome = nome;
            DataNascimento = dataNascimento;
            Status = status;    
            DataAtualizacao = dataAtualizacao;
            DataCadastro = dataCadastro;

            IsValid();
        }


        protected  override void IsValid()
        {
            DomainValidationException.When(string.IsNullOrEmpty(Nome), "Obrigatorio informar campo Nome.");
            DomainValidationException.When(!IsValidaDataNascimento(DataNascimento), "Data Nascimento informada é inválida.");
        }

        public void Inserir(string nome, DateTime dataNascimento)
        {
            Codigo = GerarCodigoPessoa();
            Nome = nome;
            DataNascimento = dataNascimento;
            Status = true;
            DataCadastro = DateTime.Now;

            IsValid();
        }

        public void Atualizar(int id, Guid codigo, string nome, DateTime dataNascimento, bool status)
        {
            Id = id;
            Codigo = codigo;
            Nome = nome;
            DataNascimento = dataNascimento;
            Status = status;
            DataAtualizacao = DateTime.Now;

            IsValid();
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

        private static bool IsValidaDataNascimento(DateTime dataNascimento)
            => dataNascimento <= DateTime.Now.AddYears(-16);

        private static Guid GerarCodigoPessoa() => Guid.NewGuid();

        
        
    }
}
