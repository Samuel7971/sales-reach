using SalesReach.Domain.Utils;
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
        public Documento Documento { get; private set; }
        public Endereco Endreco { get; private set; }

        public Pessoa() { } //Util para o Dapper
        protected Pessoa(int id, string nome, DateTime dataNascimento, DateTime? dataAtualizacao, DateTime? dataCadastro)
            : base(id, dataAtualizacao, dataCadastro)
        {
            Id = id;
            Nome = nome;
            DataNascimento = dataNascimento;
            DataAtualizacao = dataAtualizacao;
            DataCadastro = dataCadastro;
        }

        public static Pessoa Criar(int id, string nome, DateTime dataNascimento)
        {
            EhValido(nome, dataNascimento);
            return new(id, nome, dataNascimento, null, DateTime.Now);
        }

        public void Atualizar(int id, string nome, DateTime dataNascimento)
        {
            EhValido(id);
            EhValido(nome, dataNascimento);

            Id = id;
            Nome = nome;
            DataNascimento = dataNascimento;
            DataAtualizacao = DateTime.Now;
        }

        private static void EhValido(string nome, DateTime dataNascimento)
        {
            DomainValidationException.When(string.IsNullOrEmpty(nome) && nome.Length > 30, "Nome é obrigatório e deve conter no máximo 30 caracteres.");
            DomainValidationException.When(!EhValidaDataNascimento(dataNascimento), "Data Nascimento informada é inválida.");
        }

        protected static void EhValido(int id) 
            => DomainValidationException.When(id <= 0, "Obrigatório informar Id.");
            
        public override bool Equals(object obj)
        {
            var compareTo = obj as Pessoa;

            if (ReferenceEquals(this, compareTo)) return true;
            if (ReferenceEquals(null, compareTo)) return false;

            return Id.Equals(compareTo.Id);
        }

        public static bool operator ==(Pessoa a, Pessoa b)
        {
            if (ReferenceEquals(a, null) && ReferenceEquals(b, null)) return true;
            if (ReferenceEquals(a, null) || ReferenceEquals(b, null)) return false;

            return a.Equals(b);
        }

        public static bool operator !=(Pessoa a, Pessoa b) => !(a == b);

        public override int GetHashCode() =>(GetType().GetHashCode() * 907) + Id.GetHashCode();
      
        private static bool EhValidaDataNascimento(DateTime dataNascimento)
            => dataNascimento <= DateTime.Now.AddYears(-16);

        public static implicit operator string(Pessoa pessoa)
            => $@"{pessoa.Id}, {pessoa.Nome}, {string.Format("dd-MM-yyyy", pessoa.DataNascimento)}, {ToConvert.DateTimeNullable(pessoa.DataAtualizacao)}, {ToConvert.DateTimeNullable(pessoa.DataCadastro)}";

        public static implicit operator Pessoa(string strPessoa)
        {
            var data = strPessoa.Split(',');
            return new Pessoa(
                                 id: int.Parse(data[0]),
                                 nome: data[1],
                                 dataNascimento: ToConvert.StringToDateTime(data[2]),
                                 dataAtualizacao: ToConvert.StringNullable(data[3]),
                                 dataCadastro: ToConvert.StringNullable(data[4])
                              );
        }
    }
}
