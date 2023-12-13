using SalesReach.Domain.Enums;
using SalesReach.Domain.Validations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.RegularExpressions;

namespace SalesReach.Domain.ValueObjects
{
    [Table("Pessoa_Documento")]
    public record class Documento
    {
        public Documento() { }
        public Documento(int pessoaId, string numeroDocumento)
        {
            PessoaId = pessoaId;
            NumeroDocumento = numeroDocumento;
        }

        public int PessoaId { get; private set; }
        public DocumentoTipo DocumentoTipo { get; private set; }
        public string NumeroDocumento { get; private set; } 
        public bool Status { get; private set; } 
        public DateTime? DataAtualizacao { get; private set; }
        public DateTime DataCadastro { get; private set; }

        private void IsValidoDocumento(int pessoaId, string numeroDocumento)
        {
            DomainValidationException.When(pessoaId <= 0, "Pessoa Id é requerido.");
            DomainValidationException.When(
                                            !Regex.IsMatch(numeroDocumento, "^([0-9]{3}\\.?[0-9]{3}\\.?[0-9]{3}\\-?[0-9]{2})?$") 
                                            || 
                                            !Regex.IsMatch(numeroDocumento, "^([0-9]{2}\\.?[0-9]{3}\\.?[0-9]{3}\\/?[0-9]{4}\\-?[0-9]{2})$"), 
                                            "Número documento informado é inválido."
                                          );
        }

        private DocumentoTipo VerificaTipoDocumento(string numeroDocumento)
            => Regex.IsMatch(numeroDocumento, "^([0-9]{2}\\.?[0-9]{3}\\.?[0-9]{3}\\/?[0-9]{4}\\-?[0-9]{2})$") ?
                DocumentoTipo.CNPJ : DocumentoTipo.CPF;

        public void Inserir(int pessoaId, string numeroDocumento)
        {
            IsValidoDocumento(pessoaId, numeroDocumento);

            PessoaId = pessoaId;
            DocumentoTipo = VerificaTipoDocumento(numeroDocumento);
            NumeroDocumento = ReplaceNumeroDocumento(numeroDocumento);
            Status = true;
            DataAtualizacao = null;
            DataCadastro = DateTime.Now;
        }

        public Documento Atualizar(Documento documento, string numeroDocumento)
        {
            IsValidoDocumento(documento.PessoaId, numeroDocumento);

            return documento with {
                                     DocumentoTipo = VerificaTipoDocumento(documento.NumeroDocumento), 
                                     NumeroDocumento = numeroDocumento, 
                                     Status = true, 
                                     DataAtualizacao = DateTime.Now 
                                  };
          
        }

        public Documento InativarDocumento(Documento documento)
        {
            documento.Status = false;
            documento.DataAtualizacao = DateTime.Now;
            return documento;
        }

        private static string ReplaceNumeroDocumento(string numeroDocumento) => numeroDocumento.Replace(".", string.Empty)
                                                                                               .Replace("-", string.Empty)
                                                                                               .Replace("/", string.Empty);

        private static string NormalizarNumeroDocumento(string numeroDocumento) 
            => Regex.IsMatch(numeroDocumento, "^([0-9]{3}\\.?[0-9]{3}\\.?[0-9]{3}\\-?[0-9]{2})?$") ? 
                                  Convert.ToUInt64(numeroDocumento).ToString(@"000\.000\.000\-00") : 
                                  Convert.ToUInt64((string)numeroDocumento).ToString(@"00\.000\.000\/0000\-00");
        
    }
}
