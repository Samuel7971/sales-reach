using AutoMapper;
using SalesReach.Application.Models;
using SalesReach.Domain.Entities;
using SalesReach.Domain.Enums.Extensions;
using SalesReach.Domain.ValueObjects;

namespace SalesReach.Application.Mappings.DomainToModel
{
    public class DomainToModel : Profile
    {
        public DomainToModel()
        {
            CreateMap<Pessoa, PessoaModel>();
            CreateMap<Documento,  DocumentoModel>()
                .ForMember(model => model.DocumentoTipo, dom => dom.MapFrom(d => EnumDocumentoTipoExtension.ToStringDocumentoTipo(d.DocumentoTipoId)));
            CreateMap<Endereco, EnderecoModel>();
        }
    }
}
