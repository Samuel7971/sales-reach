using SalesReach.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesReach.Domain.Repositories.Interface
{
    public interface IPessoaRepository
    {
        Task<IEnumerable<Pessoa>> BuscarTodosAsync();
        Task<Pessoa> BuscarPorIdAsync(int Id);
        Task<Pessoa> BuscarPorNomeAsync(string nome);
        Task<int> InserirAsync(Pessoa pessoa);
        Task<int> AtualizarAsync(Pessoa pessoa);
        Task<int> AtualizarStatusAsync(int Id, bool status);
    }
}
