using AutoMapper;
using SalesReach.Application.Models;
using SalesReach.Application.Models.Creation;
using SalesReach.Application.Services.Interfaces;
using SalesReach.Domain.Enums;
using SalesReach.Domain.Repositories.Interface;
using SalesReach.Domain.ValueObjects;

namespace SalesReach.Application.Services
{
    public class DocumentoService : IDocumentoService
    {
        private readonly IDocumentoRepository _documentoRepository;
        private readonly IMapper _mapper;
     
        public DocumentoService(IDocumentoRepository documentoRepository, IMapper mapper)
        {
            _documentoRepository = documentoRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<DocumentoModel>> BuscarTodosAsync()
        {
            var listaDocumentos = new List<DocumentoModel>();

            var documentos = await _documentoRepository.BuscatTodosAsync();

            foreach (var documento in documentos)
            {
                documento.Buscar(documento.PessoaId, documento.Codigo, documento.DocumentoTipoId, documento.NumeroDocumento, documento.Status, documento.DataAtualizacao, documento.DataCadastro);
                listaDocumentos.Add(_mapper.Map<DocumentoModel>(documento));
            }

            return listaDocumentos;
        }

        public async Task<DocumentoModel> BuscarPorPessoaIdAsync(int pessoaId)
        {
            var documento = await _documentoRepository.BuscarPorPessoaIdAsync(pessoaId);
            documento.Buscar(documento.PessoaId, documento.Codigo, documento.DocumentoTipoId, documento.NumeroDocumento, documento.Status, documento.DataAtualizacao, documento.DataCadastro);

            return _mapper.Map<DocumentoModel>(documento);
        } 

        //TODO: Verificar com atualizar record 
        public async Task<int> Atualizar(DocumentoModel documentoModel)
        {
            var documento = new Documento();

            documento.Atualizar(documento.PessoaId, documentoModel.Codigo, documentoModel.NumeroDocumento);
            return await _documentoRepository.AtualizarAsync(documento);
        }

        public async Task<int> Inserir(DocumentoCreateModel documentoModel)
        {
            var documento = new Documento();

            if (await _documentoRepository.VerificarSeExistePorNumeroDocumentoAsync(documentoModel.NumeroDocumento))
                throw new Exception("Número de documento já cadastrado.");

            documento.Inserir(documentoModel.PessoaId, documentoModel.Codigo, documentoModel.NumeroDocumento);
            return await _documentoRepository.InserirAsync(documento);
        }

    }
}
