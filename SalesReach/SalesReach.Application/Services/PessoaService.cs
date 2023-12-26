using SalesReach.Application.Models.Creation;
using SalesReach.Application.Services.Interfaces;
using SalesReach.Domain.Entities;
using SalesReach.Domain.Repositories.Interface;

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
            //TODO: Verficar utilização do método de verificar documento (Exception)
            if (await _documentoService.VerificarSeExistePorNumeroDocumentoAsync(pessoaModel.Documento.NumeroDocumento))
                throw new Exception("Já exite pessoa com o mesmo número de documento cadastrado.");

            var novaPessoa = Pessoa.Criar(0, pessoaModel.Nome, pessoaModel.DataNascimento, status: true, dataAtualizacao: null, dataCadastro: DateTime.Now);

            await _unitOfWork.BeginTransation();

            var pessoaId = await _pessoaRepository.InserirAsync(novaPessoa);

            //Recuperando pessoa Inserida
            var pessoa = await _pessoaRepository.BuscarPorIdAsync(pessoaId);

            pessoaModel.Documento.PessoaId = pessoa.Id;
            pessoaModel.Documento.Codigo = pessoa.Codigo;
            pessoaModel.Endereco.PessoaId = pessoa.Id;
            pessoaModel.Endereco.Codigo = pessoa.Codigo;

            _= await _documentoService.Inserir(pessoaModel.Documento);

            //TODO: Endereço inserir

            await _unitOfWork.Commit();

            return pessoaId;
        }

    }
}
