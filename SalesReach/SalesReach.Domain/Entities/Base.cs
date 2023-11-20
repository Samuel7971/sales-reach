using System.ComponentModel.DataAnnotations;
using System.Drawing;

namespace SalesReach.Domain.Entities
{
    public class Base
    {
        [Key]
        public int Id { get; set; }
        public bool Status { get; set; }
        public DateTime? DataAtualizacao { get; set; }
        public DateTime DataCadastro { get; set; } = DateTime.Now;

        public Base()
        {
            Status = true;
        }
    }
}
