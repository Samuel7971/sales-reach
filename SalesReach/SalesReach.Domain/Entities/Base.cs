namespace SalesReach.Domain.Entities
{
    public abstract class Base
    {
        protected int Id { get; set; }
        public DateTime? DataAtualizacao { get; set; }
        public DateTime? DataCadastro { get; set; }

        public Base() { } //Util para o Dapper
        public Base(int id, DateTime? dataAtualizacao, DateTime? dataCadastro)
        {
            Id = id;
            DataAtualizacao = dataAtualizacao;
            DataCadastro = dataCadastro;
        }

        public virtual bool EhValido()
        {
            throw new NotImplementedException();
        }

        public override bool Equals(object obj)
        {
            var compareTo = obj as Base;

            if (ReferenceEquals(this, compareTo)) return true;
            if (ReferenceEquals(null, compareTo)) return false;

            return Id.Equals(compareTo.Id);
        }

        public static bool operator ==(Base a, Base b)
        {
            if (ReferenceEquals(a, null) && ReferenceEquals(b, null)) return true;
            if (ReferenceEquals(a, null) || ReferenceEquals(b, null)) return false;

            return a.Equals(b);
        }

        public static bool operator !=(Base a, Base b) => !(a == b);

        public override int GetHashCode() => (GetType().GetHashCode() * 907) + Id.GetHashCode();
     
        public override string ToString()
        {
            return GetType().Name + "[Id = " + Id + ",Data Atualização: " + DataAtualizacao.ToString() + ",Data Cadastro: " + DataCadastro +  "]";
        }
    }
}
