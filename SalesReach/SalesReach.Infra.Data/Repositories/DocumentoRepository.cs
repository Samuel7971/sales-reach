using Dapper;
using SalesReach.Domain.Repositories.Interface;
using SalesReach.Domain.ValueObjects;

namespace SalesReach.Infra.Data.Repositories
{
    public class DocumentoRepository : IDocumentoRepository
    {
        private DbSession _session;

        public DocumentoRepository(DbSession session)
        {
            _session = session;
        }

        public async Task<IEnumerable<Documento>> BuscatTodosAsync()
        {
            var sql = $@"SELECT 
                              PessoaId
                             ,DocumentoTipoId
                             ,NumeroDocumento
                             ,[Status]
                             ,DataAtualizacao
                             ,DataCadastro
                         FROM FitCard_Gestao..Documento_Samuel";
            //TODO: Criar Paginação
            return await _session.Connection.QueryAsync<Documento>(sql);
        }

        public async Task<Documento> BuscarPorPessoaIdAsync(int pessoaId)
        {
            var sql = $@"SELECT 
                              PessoaId
                             ,DocumentoTipoId
                             ,NumeroDocumento
                             ,[Status]
                             ,DataAtualizacao
                             ,DataCadastro
                          FROM FitCard_Gestao..Documento_Samuel 
                          WHERE PessoaId = @pessoaId";
            return await _session.Connection.QueryFirstAsync<Documento>(sql, new { pessoaId });
        }

        public async Task<Documento> BuscarPorNumeroDocumentoAsync(string numeroDocumento)
        {
            var sql = $@"SELECT 
                              PessoaId
                             ,DocumentoTipoId
                             ,NumeroDocumento
                             ,[Status]
                             ,DataAtualizacao
                             ,DataCadastro
                         FROM FitCard_Gestao..Documento_Samuel 
                         WHERE NumeroDocumento LIKE '%' + REPLACE(@numeroDocumento,'&','%') + '%';";
            return await _session.Connection.QuerySingleOrDefaultAsync<Documento>(sql, new { numeroDocumento });
        }
    

        public async Task<int> AtualizarAsync(Documento documento)
        {
            var sql = $@"UPDATE FitCard_Gestao..Documento_Samuel 
                               SET DocumentoTipoId = @DocumentoTipoId,
                                   NumeroDocumento = @NumeroDocumento,
                                   Status = @Status,
                                   DataAtualizacao = GETDATE()
                             WHERE PessoaId = @PessoaId";
            return await _session.Connection.ExecuteAsync(sql, documento);
        }

        public async Task<int> AtualizarStatusAsync(int pessoaId, bool status)
        {
            var sql = $@"UPDATE FitCard_Gestao..Documento_Samuel 
                               SET Status = @Status,
                                   DataAtualizacao = GETDATE()
                             WHERE PessoaId = @pessoaId";
            return await _session.Connection.ExecuteAsync(sql, new {pessoaId, status });
        }


        public async Task<int> InserirAsync(Documento documento)
        {
            var sql = $@"INSERT INTO FitCard_Gestao..Documento_Samuel(PessoaId, DocumentoTipoId, NumeroDocumento, Status, DataCadastro)
                                   VALUES(@PessoaId, @DocumentoTipoId, @NumeroDocumento, @Status, GETDATE())";
            return await _session.Connection.ExecuteAsync(sql, documento, _session.Transaction);
        }

        public Task<bool> VerificarSeExistePorNumeroDocumentoAsync(string numeroDocumento)
        {
            throw new NotImplementedException();
        }
    }
}
