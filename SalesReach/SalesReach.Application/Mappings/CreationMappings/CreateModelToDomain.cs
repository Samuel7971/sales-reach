using AutoMapper;
using SalesReach.Application.Models.Creation;
using SalesReach.Domain.Entities;
using SalesReach.Domain.ValueObjects;

namespace SalesReach.Application.Mappings.CreationMappings
{
    public class CreateModelToDomain : Profile
    {
        public CreateModelToDomain()
        {
            CreateMap<PessoaCreateModel, Pessoa>();
            CreateMap<DocumentoCreateModel, Documento>();
            CreateMap<EnderecoCreateModel, Endereco>();
        }
    }
}
