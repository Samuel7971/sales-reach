using SalesReach.Domain.Enums;
using SalesReach.Domain.Enums.Extensions;
using SalesReach.Domain.Validations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Net.NetworkInformation;
using System.Text.RegularExpressions;

namespace SalesReach.Domain.ValueObjects
{
    [Table("Documento_Samuel")]
    public record class Documento : BaseValueObject
    {
        public int PessoaId { get; init; }
        public int DocumentoTipoId { get; init; }
        public string NumeroDocumento { get; init; }

        public Documento() { }
        private Documento(int pessoaId, Guid codigo, string numeroDocumento)
        {
            IsValidoDocumento(pessoaId, codigo, numeroDocumento);

            PessoaId = pessoaId;
            Codigo = codigo;
            DocumentoTipoId = VerificaDocumentoTipo(numeroDocumento);
            NumeroDocumento = ReplaceNumeroDocumento(numeroDocumento);
        }
      
        private void IsValidoDocumento(int pessoaId, Guid codigo, string numeroDocumento)
        {
            DomainValidationException.When(pessoaId <= 0, "Pessoa Id é requerido.");
            DomainValidationException.When(codigo == Guid.Empty, "Código é requerido.");
            DomainValidationException.When(!Regex.IsMatch(numeroDocumento, "^([0-9]{3}\\.?[0-9]{3}\\.?[0-9]{3}\\-?[0-9]{2})?$") ||
                                           !Regex.IsMatch(numeroDocumento, "^([0-9]{2}\\.?[0-9]{3}\\.?[0-9]{3}\\/?[0-9]{4}\\-?[0-9]{2})$") ||
                                           !Regex.IsMatch(numeroDocumento, "^([0-9]{2}\\.?[0-9]{3}\\.?[0-9]{3}\\-?[0-9]{1})$"), "Número documento informado é inválido.");
        }

        public void Buscar(int pessoaId, Guid codigo, int documentoTipoId, string numeroDocumento, bool status, DateTime? dataAtualizacao, DateTime dataCadastro)
        {
            _ = new Documento()
            {
                PessoaId = pessoaId,
                Codigo = codigo,
                DocumentoTipoId = documentoTipoId,
                NumeroDocumento = NormalizarNumeroDocumento(documentoTipoId, numeroDocumento),
                Status = status,
                DataAtualizacao = dataAtualizacao,
                DataCadastro = dataCadastro
            };
        }

        public static Documento Criar(int pessoaId, Guid codigo, string numeroDocumento)
            => new(pessoaId, codigo, numeroDocumento);

        public static Documento Atualizar(Documento documento)
            => documento with { DocumentoTipoId = documento.DocumentoTipoId, NumeroDocumento = documento.NumeroDocumento };

        public static Documento AtualizarStatus(Documento documento) =>  documento with { Status = documento.Status };

        public static Documento Inserir(int pessoaId, Guid codigo, string numeroDocumento)
            => new(pessoaId, codigo, numeroDocumento);
         
        private static int VerificaDocumentoTipo(string numeroDocumento)
        {
            if (Regex.IsMatch(numeroDocumento, "^([0-9]{2}\\.?[0-9]{3}\\.?[0-9]{3}\\/?[0-9]{4}\\-?[0-9]{2})$"))
                return (int)DocumentoTipo.CNPJ;

            if (Regex.IsMatch(numeroDocumento, "^([0-9]{3}\\.?[0-9]{3}\\.?[0-9]{3}\\-?[0-9]{2})?$"))
                return (int)DocumentoTipo.CPF;

            return (int)DocumentoTipo.RG;
        }

        private static string ReplaceNumeroDocumento(string numeroDocumento) => numeroDocumento.Replace(".", string.Empty)
                                                                                               .Replace("-", string.Empty)
                                                                                               .Replace("/", string.Empty);

        private static string NormalizarNumeroDocumento(int documentoTipoId, string numeroDocumento)
        {
            var numero = string.Empty;
            switch (documentoTipoId)
            {
                case int tipoId when tipoId == 1:
                    numero = Convert.ToUInt64(numeroDocumento).ToString(@"000\.000\.000\-00");
                    break;
                case int tipoId when tipoId == 2:
                    numero = Convert.ToUInt64(numeroDocumento).ToString(@"00\.000\.000\/0000\-00");
                    break;
                case int tipoId when tipoId == 3:
                    numero = Convert.ToUInt64(numeroDocumento).ToString(@"00\.000\.000\-0");
                    break;
            }
            return numero;
        }

        public override bool Equals(Documento documento)
        => documento.PessoaId.Equals(PessoaId) && documento.Codigo.Equals(Codigo) && 
            documento.DocumentoTipoId.Equals(DocumentoTipoId) && documento.NumeroDocumento.Equals(NumeroDocumento);

        public override int GetHashCode() => base.GetHashCode();


    }
}
