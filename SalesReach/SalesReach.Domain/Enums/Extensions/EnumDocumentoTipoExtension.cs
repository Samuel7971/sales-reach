namespace SalesReach.Domain.Enums.Extensions
{
    public static class EnumDocumentoTipoExtension
    {
        public static string ToStringDocumentoTipo(this int documentoTipoId)
           => documentoTipoId switch
           {
               1 => nameof(EnumDocumentoTipo.CPF),
               2 => nameof(EnumDocumentoTipo.CNPJ),
               _ => throw new ArgumentOutOfRangeException(nameof(EnumDocumentoTipo), "Tipo de documento não cadastrado."),
           };

        public static int IntParseDocumentoTipo(this string documentoTipo)
            => documentoTipo switch
            {
                "CPF" => (int)EnumDocumentoTipo.CPF,
                "CNPJ" => (int)EnumDocumentoTipo.CNPJ,
                _ => throw new ArgumentOutOfRangeException(nameof(EnumDocumentoTipo), "Tipo de documento não cadastrado."),
            };

        public static EnumDocumentoTipo ToEnumDocumentoTipo(this string documentoTipo)
            => documentoTipo switch
            {
                "CPF" => EnumDocumentoTipo.CPF,
                "CNPJ" => EnumDocumentoTipo.CNPJ,
                _=> throw new ArgumentOutOfRangeException(nameof(EnumDocumentoTipo), "Tipo de documento não cadastrado.")
            };
    }
}
