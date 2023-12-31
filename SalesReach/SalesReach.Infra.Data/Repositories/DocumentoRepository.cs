﻿using Dapper;
using SalesReach.Domain.Repositories.Interface;
using SalesReach.Domain.ValueObjects;

namespace SalesReach.Infra.Data.Repositories
{
    public class DocumentoRepository : IDocumentoRepository
    {
        private DbSession _session;

        public DocumentoRepository(DbSession session) =>_session = session;
       
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
                             ,Codigo
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
                             ,Codigo 
                             ,DocumentoTipoId
                             ,NumeroDocumento
                             ,[Status]
                             ,DataAtualizacao
                             ,DataCadastro
                         FROM FitCard_Gestao..Documento_Samuel 
                         WHERE NumeroDocumento = @numeroDocumento";
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

        public async Task<int> AtualizarStatusAsync(int pessoaId, Guid codigo, bool status)
        {
            var sql = $@"UPDATE FitCard_Gestao..Documento_Samuel 
                               SET Status = @Status,
                                   DataAtualizacao = GETDATE()
                             WHERE PessoaId = @pessoaId AND Codigo = @codigo";
            return await _session.Connection.ExecuteAsync(sql, new {pessoaId, codigo, status });
        }


        public async Task<int> InserirAsync(Documento documento)
        {
            var sql = $@"INSERT INTO FitCard_Gestao..Documento_Samuel(PessoaId, Codigo, DocumentoTipoId, NumeroDocumento, Status, DataAtualizacao, DataCadastro)
                                   VALUES(@PessoaId, @Codigo, @DocumentoTipoId, @NumeroDocumento, @Status, null, GETDATE())";
            return await _session.Connection.ExecuteAsync(sql, documento, _session.Transaction);
        }

        public async Task<bool> VerificarSeExistePorNumeroDocumentoAsync(string numeroDocumento)
        {
            var sql = $@"SELECT NumeroDocumento FROM FitCard_Gestao..Documento_Samuel WHERE NumeroDocumento = @numeroDocumento";
            var retorno =  await _session.Connection.QueryFirstAsync<string>(sql, new { numeroDocumento });
            return string.IsNullOrEmpty(retorno);
        }
    }
}
