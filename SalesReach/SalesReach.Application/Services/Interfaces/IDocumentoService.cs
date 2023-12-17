using SalesReach.Application.Models;
using SalesReach.Application.Models.Creation;

namespace SalesReach.Application.Services.Interfaces
{
    public interface IDocumentoService
    {
        Task<IEnumerable<DocumentoModel>> BuscarTodosAsync();
        Task<DocumentoModel> BuscarPorPessoaIdAsync(int pessoaId);
        Task<int> Atualizar(DocumentoModel documentoModel);
        Task<int> Inserir(DocumentoCreateModel documentoModel);
    }
}
