using SalesReach.Domain.Enums;
using SalesReach.Domain.Enums.Extensions;
using SalesReach.Domain.Validations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesReach.Domain.ValueObjects
{
    [Table("Pessoa_Documento")]
    public record class Documento
    {
        public Documento(int pessoaId, string numeroDocumento, DocumentoTipo documentoTipo, DateTime dataAtualizacao, DateTime dataCadastro)
        {
            PessoaId = pessoaId;
            NumeroDocumento = numeroDocumento;
            DocumentoTipo = documentoTipo;
            DataAtualizacao = dataAtualizacao;
            DataCadastro = dataCadastro;
        }

        public int PessoaId { get; set; }
        public string NumeroDocumento { get; set; }
        public DocumentoTipo DocumentoTipo { get; set; }
        public DateTime DataAtualizacao { get; set; }
        public DateTime DataCadastro { get; set; }

        private void IsValidoEndereco(int pessoaId, string numeroDocumento, DocumentoTipo documentoTipo)
        {
            DomainValidationException.When(pessoaId <= 0, "Pessoa Id é requerido.");
            DomainValidationException.When(string.IsNullOrEmpty(numeroDocumento), "Número documento é requerido.");
            DomainValidationException.When(string.IsNullOrEmpty(documentoTipo.DisplayName()), "Tipo documento é requerido.");
        }

        public void Inserir(int pessoaId, string numeroDocumento, DocumentoTipo documentoTipo)
        {
            IsValidoEndereco(pessoaId, numeroDocumento, documentoTipo);

            PessoaId = pessoaId;
            NumeroDocumento = numeroDocumento;
            DocumentoTipo = documentoTipo;
            DataCadastro = DateTime.Now;
        }
    }
}
