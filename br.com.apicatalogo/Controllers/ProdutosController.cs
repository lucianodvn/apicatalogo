using AutoMapper;
using br.com.apicatalogo.DTOs;
using br.com.apicatalogo.Models;
using br.com.apicatalogo.Repositories;
using br.com.apicatalogo.Repositories.Interfaces;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace br.com.apicatalogo.Controllers
{
    [Route("[controller]")]
    public class ProdutosController : ControllerBase
    {
        //pode-se usar somente IProdutoRepository pois ja contem IRepository
        private readonly IUnitOfWork _unitOfWork;
        private readonly IProdutoRepository _produtoRepository;
        private readonly IMapper _mapper;

        public ProdutosController(IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<IEnumerable<ProdutoDTO>> Get()
        {
            var produtos = _unitOfWork.ProdutoRepository.GetAll();
            if (produtos is null)
            {
                return NotFound("Não há produtos.");
            }

            var produtosDto = _mapper.Map<IEnumerable<ProdutoDTO>>(produtos);

            return Ok(produtosDto);
        }


        [HttpGet("{id:int}", Name = "ObterProduto")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<ProdutoDTO> Get(int id)
        {
            var produto = _unitOfWork.ProdutoRepository.Get(p => p.IdProduto == id);
            if (produto is null)
            {
                return NotFound("Produto não encontrado.");
            }

            var produtoDto = _mapper.Map<ProdutoDTO>(produto);

            return Ok(produtoDto);
        }

        [HttpGet("produtos/{id}")]
        public ActionResult<IEnumerable<ProdutoDTO>> GetProdutosCategorias(int id)
        {
            var produtos = _produtoRepository.GetProdutosPorCategoria(id);
            if (produtos is null)
            {
                return NotFound($"Produtos por categoriaId = {id} não encontrado");
            }

            var produtosDto = _mapper.Map<IEnumerable<ProdutoDTO>>(produtos);

            return Ok(produtosDto);
        }

        [HttpPost]
        public ActionResult<ProdutoDTO> Post([FromBody] ProdutoDTO produtoDto)
        {
            if (produtoDto is null)
            {
                return BadRequest("Produto não encontrado");
            }

            var produto = _mapper.Map<Produto>(produtoDto);

            var novoProduto = _unitOfWork.ProdutoRepository.Create(produto);
            _unitOfWork.SalvarAlteracoes();

            var novoProdutoDto = _mapper.Map<ProdutoDTO>(novoProduto);

            return new CreatedAtRouteResult("ObterProduto", new { id = novoProdutoDto.IdProduto }, novoProdutoDto);
        }

        [HttpPut("{id:int}")]
        public ActionResult<ProdutoDTO> Put(int id, [FromBody] ProdutoDTO produtoDto)
        {
            if (id != produtoDto.IdProduto)
            {
                return BadRequest("Produto não encontrado");
            }

            var produtoAtualizado = _mapper.Map<Produto>(produtoDto);

            var produtoAtualizadoDto = _unitOfWork.ProdutoRepository.Update(produtoAtualizado);

            _unitOfWork.SalvarAlteracoes();

            return Ok(produtoAtualizadoDto);
        }

        [HttpDelete("{id}")]
        public ActionResult<ProdutoDTO> Delete(int id)
        {
            var produto = _unitOfWork.ProdutoRepository.Get(p => p.IdProduto == id);
            if (produto is null)
            {
                return NotFound($"Produto de id={id} não encontrado.");
            }

            var produtoDeletado = _unitOfWork.ProdutoRepository.Delete(produto);
            _unitOfWork.SalvarAlteracoes();

            var produtoDeletadoDto = _mapper.Map<ProdutoDTO>(produtoDeletado);

            return Ok(produto);
        }

        [HttpPatch("{id}/updateparcial")]
        public ActionResult<ProdutoDtoUpdateResponse> Patch(int id, [FromBody]
            JsonPatchDocument<ProdutoDtoUpdateRequest> patchProdutoDto)
        {
            if (patchProdutoDto is null || id <= 0)
            {
                return BadRequest();
            }

            var produto = _unitOfWork.ProdutoRepository.Get(c => c.IdProduto == id);

            if (produto is null)
            {
                return NotFound();
            }

            var produtoUpdateRequest = _mapper.Map<ProdutoDtoUpdateRequest>(produto);

            patchProdutoDto.ApplyTo(produtoUpdateRequest, ModelState);

            if (!ModelState.IsValid || TryValidateModel(produtoUpdateRequest))
            {
                return BadRequest(ModelState);
            }

            _mapper.Map(produtoUpdateRequest, produto);

            _unitOfWork.ProdutoRepository.Update(produto);
            _unitOfWork.SalvarAlteracoes();

            return Ok(_mapper.Map<ProdutoDtoUpdateResponse>(produto));
        }
    }
}

