using SalesReach.Application.Models.Creation;
using SalesReach.Application.Services.Interfaces;
using SalesReach.Domain.Entities;
using SalesReach.Domain.Repositories.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesReach.Application.Services
{
    public class PessoaService : IPessoaService
    {
        private readonly IPessoaRepository _pessoaRepository;
        private readonly IDocumentoService _documentoService;
        private readonly IUnitOfWork _unitOfWork;

        public PessoaService(IPessoaRepository pessoaRepository, IDocumentoService documentoService,IUnitOfWork unitOfWork)
        {
            _pessoaRepository = pessoaRepository;
            _documentoService = documentoService;
            _unitOfWork = unitOfWork;

        }

        //TODO: Terminar o inserte de pessoa
        public async Task<int> InserirAsync(PessoaCreateModel pessoaModel)
        {
            var novaPessoa = new Pessoa();

            novaPessoa.Inserir(pessoaModel.Nome, pessoaModel.DataNascimento);

            await _unitOfWork.BeginTransation();

            var pessoaId = await _pessoaRepository.InserirAsync(novaPessoa);

            //Recuperando pessoa Inserida
            var pessoa = await _pessoaRepository.BuscarPorIdAsync(pessoaId);

            pessoaModel.Documento.PessoaId = pessoa.Id;
            pessoaModel.Documento.Codigo = pessoa.Codigo;
            pessoaModel.Endereco.PessoaId = pessoa.Id;
            pessoaModel.Endereco.Codigo = pessoa.Codigo;

            _= await _documentoService.Inserir(pessoaModel.Documento);

            await _unitOfWork.Commit();

            return pessoaId;
        }

    }
}
