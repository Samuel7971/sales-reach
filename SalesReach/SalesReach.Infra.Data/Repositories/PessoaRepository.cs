using Dapper;
using SalesReach.Domain.Entities;
using SalesReach.Domain.Repositories.Interface;

namespace SalesReach.Infra.Data.Repositories
{
    public class PessoaRepository : IPessoaRepository
    {
        private DbSession _session;

        public PessoaRepository(DbSession session)
        {
            _session = session;
        }

        public async Task<IEnumerable<Pessoa>> BuscarTodosAsync()
        {
            var sql = $@"SELECT 
                             Id
                            ,Nome
                            ,PessoaTipoId
                            ,DataNascimento
                            ,Status
                            ,DataAtualizacao
                            ,DataCadastro
                         FROM FitCard_Gestao..Pessoa_Samuel";
            //TODO: Criar Paginação
            return await _session.Connection.QueryAsync<Pessoa>(sql);
        }

        public async Task<Pessoa> BuscarPorIdAsync(int id)
        {
            var sql = $@"SELECT 
                             Id
                            ,Nome
                            ,PessoaTipoId
                            ,DataNascimento
                            ,Status
                            ,DataAtualizacao
                            ,DataCadastro
                         FROM FitCard_Gestao..Pessoa_Samuel WHERE Id = @id";
            return await _session.Connection.QuerySingleOrDefaultAsync<Pessoa>(sql, new { id });
        }

        public async Task<Pessoa> BuscarPorNomeAsync(string nome)
        {
            var sql = $@"SELECT 
                             Id
                            ,Nome
                            ,PessoaTipoId
                            ,DataNascimento
                            ,Status
                            ,DataAtualizacao
                            ,DataCadastro
                         FROM FitCard_Gestao..Pessoa_Samuel WHERE Nome LIKE '%' + REPLACE(@nome,'&','%') + '%'";
            return await _session.Connection.QueryFirstAsync<Pessoa>(sql, new { nome });
        }

        public async Task<int> AtualizarAsync(Pessoa pessoa)
        {
            var sql = $@"UPDATE FitCard_Gestao..Pessoa_Samuel
                               SET Nome = @Nome
                                  ,PessoaTipoId = @PessoaTipoId
                                  ,DataNascimento = @DataNascimento
                                  ,Status = @Status
                                  ,DataAtualizacao = GETDATE() 
                         WHERE Id = @Id";
            return await _session.Connection.ExecuteAsync(sql, pessoa);
        }

        public async Task<int> AtualizarStatusAsync(int id, bool status)
        {
            var sql = $@"UPDATE FitCard_Gestao..Pessoa_Samuel
                               SET Status = @status, DataAtualizacao = GETDATE()
                         WHERE Id = @id";
            return await _session.Connection.ExecuteAsync(sql, new {id, status});
        }

        public async Task<int> InserirAsync(Pessoa pessoa)
        {
            var sql = $@"INSERT INTO FitCard_Gestao..Pessoa_Samuel(Nome, PessoaTipoId, DataNascimento, Status, DataCadastro)
                                VALUES(@Nome, @PessoaTipoId, @DataNascimento, @Status, GETDATE());
                         SELECT @@IDENTITY";
            return await _session.Connection.ExecuteScalarAsync<int>(sql, pessoa, _session.Transaction);
        }
    }
}
