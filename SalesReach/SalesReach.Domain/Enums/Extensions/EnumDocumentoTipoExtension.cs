using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesReach.Domain.Enums.Extensions
{
    public static class EnumDocumentoTipoExtension
    {
        public static string ToStringDocumentoTipo(this int documentoTipoId)
           => documentoTipoId switch
           {
               1 => DocumentoTipo.CPF.DisplayName(),
               2 => DocumentoTipo.CNPJ.DisplayName(),
               3 => DocumentoTipo.RG.DisplayName(),
               _ => throw new ArgumentOutOfRangeException(nameof(DocumentoTipo), "Tipo de documento não cadastrado."),
           };

        public static int IntParseDocumentoTipo(this string documentoTipo)
            => documentoTipo switch
            {
                "CPF" => 1,
                "CNPJ" => 2,
                "RG" => 3,
                _ => throw new ArgumentOutOfRangeException(nameof(DocumentoTipo), "Tipo documento não cadastrado."),
            };
    }
}
