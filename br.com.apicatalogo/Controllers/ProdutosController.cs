using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using br.com.apicatalogo.Context;
using br.com.apicatalogo.Models;
using br.com.apicatalogo.Repositories;
using br.com.apicatalogo.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace br.com.apicatalogo.Controllers
{
    [Route("[controller]")]
    public class ProdutosController : ControllerBase
    {
        //pode-se usar somente IProdutoRepository pois ja contem IRepository
        private readonly IUnitOfWork _unitOfWork;
        private readonly IProdutoRepository _produtoRepository;

        public ProdutosController(IUnitOfWork unitOfWork,
            IProdutoRepository produtoRepository)
        {
            _unitOfWork = unitOfWork;
            _produtoRepository = produtoRepository;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Produto>> Get()
        {
            var produtos = _unitOfWork.ProdutoRepository.GetAll();
            if (produtos is null)
            {
                return NotFound("Não há produtos.");
            }
            return Ok(produtos);
        }

  
        [HttpGet("{id:int}", Name="ObterProduto")]
        public ActionResult<Produto> Get(int id)
        {
            var produto = _unitOfWork.ProdutoRepository.Get(p => p.IdProduto == id);
            if(produto is null)
            {
               return NotFound("Produto não encontrado.");
            }
            return Ok(produto);
        }

        [HttpGet("produtos/{id}")]
        public ActionResult<IEnumerable<Produto>> GetProdutosCategorias(int id)
        {
            var produtos = _produtoRepository.GetProdutosPorCategoria(id);
            if(produtos is null)
            {
                return NotFound($"Produtos por categoriaId = {id} não encontrado");
            }

            return Ok(produtos);
        }

        [HttpPost]
        public ActionResult Post([FromBody] Produto produto)
        {
            if (produto is null)
            {
                return BadRequest("Produto não encontrado");
            }

            _unitOfWork.ProdutoRepository.Create(produto);
            _unitOfWork.SalvarAlteracoes();

            return new CreatedAtRouteResult("ObterProduto", new { id = produto.IdProduto }, produto);
        }

        [HttpPut("{id:int}")]
        public ActionResult Put(int id, [FromBody] Produto produto)
        {
            if (id != produto.IdProduto)
            {
                return BadRequest("Produto não encontrado");
            }

            var produtoAtualizado = _unitOfWork.ProdutoRepository.Update(produto);
            _unitOfWork.SalvarAlteracoes();

            return Ok(produtoAtualizado);
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            var produto = _unitOfWork.ProdutoRepository.Get(p => p.IdProduto == id);
            if (produto is null)
            {
                return NotFound($"Produto de id={id} não encontrado.");
            }

            _unitOfWork.ProdutoRepository.Delete(produto);
            _unitOfWork.SalvarAlteracoes();

            return Ok(produto);
        }
    }
}

