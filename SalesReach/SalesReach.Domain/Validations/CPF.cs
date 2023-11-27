using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SalesReach.Domain.Validations
{
    public record class CPF
    {
        public string Numero {  get; set; }

        public CPF(string numero)
        {
            Numero = numero;
        }

        private void IsVaidoCPF(string numero)
        {
            DomainValidationException.When(string.IsNullOrWhiteSpace(numero), "Número cpf é requerido.");
        }

        private void ValidarCPF(string numero)
        {
            if (Regex.IsMatch(numero, "/^([0-9]{3}\\.?[0-9]{3}\\.?[0-9]{3}\\-?[0-9]{2}|[0-9]{2}\\.?[0-9]{3}\\.?[0-9]{3}\\/?[0-9]{4}\\-?[0-9]{2})$/"))
            {

            }
        }

        public void Inserir(string numero)
        {
            IsVaidoCPF(numero);

            Numero = numero;
        }
    }
}
