using AutoMapper;
using SalesReach.Application.Models;
using SalesReach.Application.Models.Creation;
using SalesReach.Application.Services.Interfaces;
using SalesReach.Domain.Repositories.Interface;
using SalesReach.Domain.ValueObjects;

namespace SalesReach.Application.Services
{
    public class DocumentoService : IDocumentoService
    {
        private readonly IDocumentoRepository _documentoRepository;
       
        public DocumentoService(IDocumentoRepository documentoRepository) => _documentoRepository = documentoRepository;

        public async Task<int> Inserir(DocumentoCreateModel documentoModel)
        {
            if (await _documentoRepository.VerificarSeExistePorNumeroDocumentoAsync(documentoModel.NumeroDocumento))
                throw new Exception("Número de documento já cadastrado.");

            return await _documentoRepository.InserirAsync(Documento.Inserir(documentoModel.PessoaId, documentoModel.Codigo, documentoModel.NumeroDocumento));
        }
    
        public async Task<IEnumerable<DocumentoModel>> BuscarTodosAsync()
        {
            var listaDocumentos = new List<DocumentoModel>();

            var documentos = await _documentoRepository.BuscatTodosAsync();

            foreach (var documento in documentos)
            {
                documento.Buscar(documento.PessoaId,
                                 documento.Codigo,
                                 documento.DocumentoTipo,
                                 documento.NumeroDocumento,
                                 documento.Status,
                                 documento.DataAtualizacao,
                                 documento.DataCadastro);

                listaDocumentos.Add((DocumentoModel)documento);
            }
            return listaDocumentos;
        }

        public async Task<DocumentoModel> BuscarPorPessoaIdAsync(int pessoaId)
        {
            var documento = await _documentoRepository.BuscarPorPessoaIdAsync(pessoaId);
            documento.Buscar(documento.PessoaId,
                             documento.Codigo,
                             documento.DocumentoTipo,
                             documento.NumeroDocumento,
                             documento.Status,
                             documento.DataAtualizacao,
                             documento.DataCadastro);

            return documento;
        }

        public async Task<int> Atualizar(DocumentoModel documentoModel)
        {
            //var documento = Documento.Criar(documentoModel.PessoaId, documentoModel.Codigo, documentoModel.NumeroDocumento);
            //var documentoBase = await _documentoRepository.BuscarPorPessoaIdAsync(documentoModel.PessoaId);

            //if (documentoBase.Equals(documento))
            //    return 0;

            //var novoDocumento = Documento.Atualizar(documento);
            //return await _documentoRepository.AtualizarAsync(novoDocumento);
            throw new NotImplementedException();
        }

        public async Task<bool> VerificarSeExistePorNumeroDocumentoAsync(string numeroDocumento)
            => await _documentoRepository.VerificarSeExistePorNumeroDocumentoAsync(numeroDocumento);
    }
}
