using AutoMapper;
using SalesReach.Application.Models;
using SalesReach.Domain.Entities;
using SalesReach.Domain.Enums.Extensions;
using SalesReach.Domain.ValueObjects;

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

            #region .: Documento Mapping
            CreateMap<DocumentoModel, Documento>()
                .ForMember(dom => dom.DocumentoTipoId, model => model.MapFrom(m => EnumDocumentoTipoExtension.IntParseDocumentoTipo(m.DocumentoTipo)));
            #endregion
        }
    }
}
