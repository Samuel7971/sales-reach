﻿using SalesReach.Domain.Enums;
using SalesReach.Domain.Enums.Extensions;
using SalesReach.Domain.Utils;
using SalesReach.Domain.Validations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.RegularExpressions;

namespace SalesReach.Domain.ValueObjects
{
    [Table("Documento_Samuel")]
    public record Documento : BaseValueObject
    {
        public int PessoaId { get; private set; }
        public EnumDocumentoTipo DocumentoTipo { get; private set; }
        public string NumeroDocumento { get; private set; }


        private Documento(int pessoaId, EnumDocumentoTipo documentoTipo, string numeroDocumento, bool status, DateTime? dataAtualizacao, DateTime? dataCadastro) 
            : base(status, dataAtualizacao, dataCadastro)
        {
            PessoaId = pessoaId;
            DocumentoTipo = documentoTipo;
            NumeroDocumento = numeroDocumento;
        }

        public static Documento Criar(int pessoaId, string numeroDocumento)
        {
            EhValido(pessoaId, numeroDocumento);

            var documentoTipo = VerificaDocumentoTipo(numeroDocumento);
            ReplaceNumeroDocumento(numeroDocumento);
            
            return new(pessoaId, documentoTipo, numeroDocumento, true, null, DateTime.Now);
        }

        public static Documento Atualizar(Documento documentoAntigo, Documento documentoNovo)
        {
            EhValido(documentoNovo.PessoaId, documentoNovo.NumeroDocumento);
            var documentoTipo = VerificaDocumentoTipo(documentoNovo.NumeroDocumento);

            if (documentoAntigo.Equals(documentoNovo))
                return documentoAntigo;

            return documentoAntigo with
            {
                DocumentoTipo = documentoTipo,
                NumeroDocumento = ReplaceNumeroDocumento(documentoNovo.NumeroDocumento),
                DataAtualizacao = null,
                DataCadastro = DateTime.Now
            };
        }

        public static Documento Inativar(Documento documento)
        {
            documento.Status = false;
            documento.DataAtualizacao = DateTime.Now;
            return documento;
        }

        private static void EhValido(int pessoaId, string numeroDocumento)
        {
            DomainValidationException.When(pessoaId <= 0, "Pessoa Id é requerido.");
            DomainValidationException.When(EhValidoNumeroDocumento(numeroDocumento), "Número documento informado é inválido.");
        }

        public static EnumDocumentoTipo VerificaDocumentoTipo(string numeroDocumento)
        {
            if (Regex.IsMatch(numeroDocumento, "^([0-9]{2}\\.?[0-9]{3}\\.?[0-9]{3}\\/?[0-9]{4}\\-?[0-9]{2})$"))
                return EnumDocumentoTipo.CNPJ;

            if (Regex.IsMatch(numeroDocumento, "^([0-9]{3}\\.?[0-9]{3}\\.?[0-9]{3}\\-?[0-9]{2})?$"))
                return EnumDocumentoTipo.CPF;

            throw new ArgumentOutOfRangeException(nameof(EnumDocumentoTipo), "Tipo de documento não cadastrado.");
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
                default:
                    throw new ArgumentOutOfRangeException(nameof(EnumDocumentoTipo), "Tipo de documento não cadastrado.");
            }
            return numero;
        }

        public static bool EhValidoNumeroDocumento(string numeroDocumento) 
            => Regex.IsMatch(numeroDocumento, "^([0-9]{2}\\.?[0-9]{3}\\.?[0-9]{3}\\/?[0-9]{4}\\-?[0-9]{2})$") || 
               Regex.IsMatch(numeroDocumento, "^([0-9]{3}\\.?[0-9]{3}\\.?[0-9]{3}\\-?[0-9]{2})?$");

        public static implicit operator string(Documento documento)
            => $@"{documento.PessoaId}, {documento.DocumentoTipo.DisplayName()}, {NormalizarNumeroDocumento((int)documento.DocumentoTipo, documento.NumeroDocumento)}, {ToConvert.DateTimeNullable(documento.DataAtualizacao)}, {ToConvert.DateTimeNullable(documento.DataCadastro)}";

        public static implicit operator Documento(string strDocumento)
        {
            var data = strDocumento.Split(',');
            return new Documento(
                           pessoaId: int.Parse(data[0]),
                           documentoTipo: VerificaDocumentoTipo(data[3]),
                           numeroDocumento: ReplaceNumeroDocumento(data[3]),
                           status: bool.Parse(data[4]),
                           dataAtualizacao: ToConvert.StringNullable(data[5]),
                           dataCadastro: ToConvert.StringNullable(data[6])
                          );
        }
    }
}
