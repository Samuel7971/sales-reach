using AutoMapper;
using SalesReach.Application.Models;
using SalesReach.Domain.Entities;

namespace SalesReach.Application.Mappings.ModelToDomain
{
    public class ModelToDomain : Profile
    {
        public ModelToDomain()
        {
            #region .: Pessoa Mapping
            CreateMap<PessoaModel, Pessoa>();
            CreateMap<IEnumerable<PessoaModel>, IEnumerable<Pessoa>>();
            #endregion
        }
    }
}
