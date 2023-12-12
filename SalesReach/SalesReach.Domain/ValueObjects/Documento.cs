using SalesReach.Domain.Enums;
using SalesReach.Domain.Enums.Extensions;
using SalesReach.Domain.Validations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

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

        public int PessoaId { get; set; }
        public string NumeroDocumento { get; set; }
        public DocumentoTipo DocumentoTipo { get; set; }
        public bool Status { get; set; } 
        public DateTime DataAtualizacao { get; set; }
        public DateTime DataCadastro { get; set; }

        private void IsValidoDocumento(int pessoaId, string numeroDocumento)
        {
            DomainValidationException.When(pessoaId <= 0, "Pessoa Id é requerido.");
            DomainValidationException.When(string.IsNullOrEmpty(numeroDocumento), "Número documento é requerido.");
        }

        private DocumentoTipo VerificaTipoDocumento(string numeroDocumento)
        {
            if (Regex.IsMatch(numeroDocumento, "^([0-9]{3}\\.?[0-9]{3}\\.?[0-9]{3}\\-?[0-9]{2})?$"))
                return DocumentoTipo.CPF;

            if (Regex.IsMatch(numeroDocumento, "^([0-9]{2}\\.?[0-9]{3}\\.?[0-9]{3}\\/?[0-9]{4}\\-?[0-9]{2})$"))
                return DocumentoTipo.CNPJ;

            throw new ArgumentOutOfRangeException(nameof(numeroDocumento), $"Número de documento informado é inválido.");
        }

        public void Inserir(int pessoaId, string numeroDocumento)
        {
            IsValidoDocumento(pessoaId, numeroDocumento);

            PessoaId = pessoaId;
            NumeroDocumento = numeroDocumento;
            DocumentoTipo = VerificaTipoDocumento(numeroDocumento);
            Status = true;
            DataCadastro = DateTime.Now;
        }

        public Documento Atualizar(Documento documento, string numeroDocumento)
        {
            IsValidoDocumento(documento.PessoaId, numeroDocumento);

            return documento with {
                                     NumeroDocumento = numeroDocumento, 
                                     DocumentoTipo = VerificaTipoDocumento(documento.NumeroDocumento), 
                                     Status = true, 
                                     DataAtualizacao = DateTime.Now 
                                  };
          
        }

        public Documento InativarDocumento(Documento documento)
        {
            documento.Status = false;
            return documento;
        }
    }
}
