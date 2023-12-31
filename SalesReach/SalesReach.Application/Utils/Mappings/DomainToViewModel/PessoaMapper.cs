using SalesReach.Application.ViewModels;
using SalesReach.Domain.Entities;

namespace SalesReach.Application.Utils.Mappings.DomainToViewModel
{
    public static class PessoaMapper
    {
        public static List<PessoaViewModel> ToListPessoaViewModel(this IEnumerable<Pessoa> listaPessoa)
        {
            var listaRetorno = new List<PessoaViewModel>();

            foreach (var pessoa in listaPessoa)
            {
                listaRetorno.Add(new PessoaViewModel()
                {
                    Id = pessoa.Id,
                    Nome = pessoa.Nome,
                    DataNascimento = pessoa.DataNascimento,
                    Status = pessoa.Status,
                    DataAtualizacao = pessoa.DataAtualizacao,
                    DataCadastro = pessoa.DataCadastro

                });
            }
            return listaRetorno ?? new List<PessoaViewModel>();
        }

        public static PessoaViewModel ToPessoaViewModel(this Pessoa pessoa) 
            => new()
            {
                Id = pessoa.Id,
                Nome = pessoa.Nome,
                DataNascimento = pessoa.DataNascimento,
                Status = pessoa.Status,
                DataAtualizacao = pessoa.DataAtualizacao,
                DataCadastro = pessoa.DataCadastro
            };
    }
}
