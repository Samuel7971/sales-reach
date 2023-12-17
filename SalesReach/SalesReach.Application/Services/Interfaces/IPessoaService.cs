using SalesReach.Application.Models.Creation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesReach.Application.Services.Interfaces
{
    public interface IPessoaService
    {
        Task<int> InserirAsync(PessoaCreateModel pessoa);
    }
}
