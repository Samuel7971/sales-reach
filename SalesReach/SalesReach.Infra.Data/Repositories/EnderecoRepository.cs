using Dapper;
using SalesReach.Domain.Repositories.Interface;
using SalesReach.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesReach.Infra.Data.Repositories
{
    public class EnderecoRepository : IEnderecoRepository
    {
        private readonly DbSession _session;

        public EnderecoRepository(DbSession session)
        {            
            _session = session;
        }

        public async Task<IEnumerable<Endereco>> BuscarTodosAsync()
        {
            var sql = $@"SELECT 
                                PessoaId
                              , CEP
                              , Logradouro
                              , Numero
                              , Complemento
                              , Bairro
                              , Localidade
                              , UF
                              , [Status]
                              , DataAtualizacao
                              , DataCadastro
                         FROM FitCard_Gestao..Endereco_Samuel";
            //TODO: Criar Paginação
            return await _session.Connection.QueryAsync<Endereco>(sql);
        }

        public async Task<Endereco> BuscarPorCEPAsync(string cep)
        {
            var sql = $@"SELECT 
                                PessoaId
                              , CEP
                              , Logradouro
                              , Numero
                              , Complemento
                              , Bairro
                              , Localidade
                              , UF
                              , [Status]
                              , DataAtualizacao
                              , DataCadastro
                         FROM FitCard_Gestao..Endereco_Samuel
                         WHERE CEP = @cep";
            return await _session.Connection.QueryFirstAsync<Endereco>(sql, new { cep });
        }

        public async Task<Endereco> BuscarPorPessoaIdAsync(int pessoaId)
        {
            var sql = $@"SELECT 
                                PessoaId
                              , CEP
                              , Logradouro
                              , Numero
                              , Complemento
                              , Bairro
                              , Localidade
                              , UF
                              , [Status]
                              , DataAtualizacao
                              , DataCadastro
                         FROM FitCard_Gestao..Endereco_Samuel
                         WHERE PessoaId = @pessoaId";
            return await _session.Connection.QueryFirstAsync<Endereco>(sql, new { pessoaId });
        }

        public async Task<IEnumerable<Endereco>> BuscarPorLogradouroAsync(string logradouro)
        {
            var sql = $@"SELECT 
                                PessoaId
                              , CEP
                              , Logradouro
                              , Numero
                              , Complemento
                              , Bairro
                              , Localidade
                              , UF
                              , [Status]
                              , DataAtualizacao
                              , DataCadastro
                         FROM FitCard_Gestao..Endereco_Samuel
                         WHERE Logradouro LIKE '%' + REPLACE(@logradouro,'&','%') + '%'";
            return await _session.Connection.QueryAsync<Endereco>(sql, new { logradouro });
        }

        public async Task<int> AtualizarAsync(Endereco endereco)
        {
            var sql = $@"UPDATE Endereco_Samuel SET
                              , CEP = @CEP
                              , Logradouro = @Logradouro
                              , Numero = @Numero
                              , Complemento = @Complemento
                              , Bairro = @Bairro
                              , Localidade = @Localidade
                              , UF = @UF
                              , [Status] = @Status
                              , DataAtulizacao = @DataAtualizacao
                         FROM FitCard_Gestao..Endereco_Samuel
                         WHERE PessoaId = @PessoaId";
            return await _session.Connection.ExecuteAsync(sql, endereco);
        }

        public async Task<int> InserirAsync(Endereco endereco)
        {
            var sql = $@"INSERT INTO FitCard_Gestao..Endereco_Samuel 
                                (PessoaId
                                 ,CEP
                                 ,Logradouro
                                 ,Numero
                                 ,Complemento
                                 ,Bairro
                                 ,Localidade
                                 ,UF
                                 ,[Status]
                                 ,DataCadastro)
                          VALUES(@PessoaId
                                 ,@CEP
                                 ,@Logradouro
                                 ,@Numero
                                 ,@Complemento
                                 ,@Bairro
                                 ,@Localidade
                                 ,@UF
                                 ,@Status
                                 ,GETDATE())";
            return await _session.Connection.ExecuteAsync(sql, endereco, _session.Transaction);
        }
    }
}
