using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesReach.Application.ViewModels
{
    public abstract class BaseViewModel
    {
        public int Id { get; set; }
        public bool Status { get; set; }
        public DateTime? DataAtualizacao { get; set; } //TODO: Ajuste data para retorno swegger
        public DateTime? DataCadastro { get; set; }
    }
}
