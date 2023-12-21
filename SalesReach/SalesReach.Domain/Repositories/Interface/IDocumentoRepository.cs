using SalesReach.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesReach.Domain.Repositories.Interface
{
    public interface IDocumentoRepository
    {
        Task<IEnumerable<Documento>> BuscatTodosAsync();
        Task<Documento> BuscarPorPessoaIdAsync(int pessoaId);
        Task<Documento> BuscarPorNumeroDocumentoAsync(string numeroDocumento);
        Task<int> InserirAsync(Documento documento);
        Task<int> AtualizarAsync(Documento documento);
        Task<int> AtualizarStatusAsync(int pessoaId, Guid codigo, bool status);
        Task<bool> VerificarSeExistePorNumeroDocumentoAsync(string numeroDocumento);
    }
}
