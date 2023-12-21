using SalesReach.Domain.Enums.Extensions;
using SalesReach.Domain.ValueObjects;

namespace SalesReach.Application.Models
{
    public record class DocumentoModel
    {
        public int PessoaId { get; set; }
        public Guid Codigo { get; set; }
        public string DocumentoTipo { get; set; }
        public string NumeroDocumento { get; set; }
        public bool Status { get; set; }
        public DateTime? DataAtualizacao { get; set; }
        public DateTime DataCadastro { get; set; }


        public static implicit operator DocumentoModel(Documento documento)
            => new()
            {
                PessoaId = documento.PessoaId,
                Codigo = documento.Codigo,
                DocumentoTipo = EnumDocumentoTipoExtension.ToStringDocumentoTipo(documento.DocumentoTipoId),
                NumeroDocumento = documento.NumeroDocumento,
                Status = documento.Status,
                DataAtualizacao = documento.DataAtualizacao,
                DataCadastro = documento.DataCadastro
            };
    }
}
