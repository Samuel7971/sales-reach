using System.ComponentModel.DataAnnotations;

namespace SalesReach.Domain.Entities
{
    public class Base
    {
        [Key]
        protected int Id { get; set; }
        protected bool Status { get; set; }
        protected DateTime? DataAtualizacao { get; set; }
        protected DateTime DataCadastro { get; set; } 
    }
}
