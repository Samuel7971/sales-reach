using SalesReach.Application.Models;
using SalesReach.Application.Models.Creation;
using SalesReach.Application.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesReach.Application.Services.Interfaces
{
    public interface IPessoaService
    {
        Task<IEnumerable<PessoaViewModel>> BuscarTodosAsync();
        Task<PessoaViewModel> BuscarPorIdAsync(int id);
        Task<PessoaViewModel> BuscaPorNomeAsync(string nome);
        Task<int> InserirAsync(PessoaCreateModel pessoa);
        Task<int> AtualizarAsync(PessoaModel pessoa);
        Task<int> AtualizarStatusAsync(int id, bool status);
    }
}
