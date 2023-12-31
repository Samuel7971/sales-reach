using SalesReach.Domain.Validations;
using SalesReach.Domain.ValueObjects;
using System.ComponentModel.DataAnnotations.Schema;

namespace SalesReach.Domain.Entities
{
    [Table("Pessoa_Samuel")]
    public class Pessoa : Base
    {
        public string Nome { get; private set; }
        public DateTime DataNascimento { get; private set; }
        public Documento Documento{ get; private set; }
        public Endereco Endreco { get; private set; }

        public Pessoa() { } //Util para o Dapper
        private Pessoa(int id, string nome, DateTime dataNascimento, bool status, DateTime? dataAtualizacao, DateTime? dataCadastro) : base(id, Guid.NewGuid(), status, dataAtualizacao, dataCadastro)
        {
            Id = id;
            Nome = nome;
            DataNascimento = dataNascimento;
            Status = status;    
            DataAtualizacao = dataAtualizacao;
            DataCadastro = dataCadastro;

            Validador();
        }

        protected override void Validador()
        {
            DomainValidationException.When(string.IsNullOrEmpty(Nome) && Nome.Length > 15, "Obrigatório informar campo Nome.");
            DomainValidationException.When(!IsValidaDataNascimento(DataNascimento), "Data Nascimento informada é inválida.");
        }
        public static Pessoa Criar(int id, string nome, DateTime dataNascimento, bool status, DateTime? dataAtualizacao, DateTime? dataCadastro)
            => new(id, nome, dataNascimento, status, dataAtualizacao, dataCadastro);
        public void Atualizar(int id, Guid codigo, string nome, DateTime dataNascimento, bool status)
        {
            Id = id;
            Codigo = codigo;
            Nome = nome;
            DataNascimento = dataNascimento;
            Status = status;
            DataAtualizacao = DateTime.Now;

            Validador();
        }
        public override void AtualizarStatus(int id, bool status) 
        {
            Id = id;
            Status = status;
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
    }
}
