using SalesReach.Domain.ValueObjects;

namespace SalesReach.Domain.Repositories.Interface
{
    public interface IEnderecoRepository
    {
        Task<IEnumerable<Endereco>> BuscarTodosAsync();
        Task<Endereco> BuscarPorCEPAsync(string cep);
        Task<Endereco> BuscarPorPessoaIdAsync(int pessoaId);
        Task<IEnumerable<Endereco>> BuscarPorLogradouroAsync(string logradouro);
        Task<int> AtualizarAsync(Endereco endereco);
        Task<int> InserirAsync(Endereco endereco);
    }
}
