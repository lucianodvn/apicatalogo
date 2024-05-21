using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using br.com.apicatalogo.Context;
using br.com.apicatalogo.Models;
using br.com.apicatalogo.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace br.com.apicatalogo.Controllers
{
    [Route("[controller]")]
    public class ProdutosController : ControllerBase
    {
        private readonly IProdutoRepository _produtoRepository;

        public ProdutosController(IProdutoRepository produtoRepository)
        {
            _produtoRepository = produtoRepository;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Produto>> Get()
        {
            var produtos = _produtoRepository.GetProdutos().ToList();
            if (produtos is null)
            {
                return NotFound("Não há produtos.");
            }
            return Ok(produtos);
        }

  
        [HttpGet("{id:int}", Name="ObterProduto")]
        public ActionResult<Produto> Get(int id)
        {
            var produto = _produtoRepository.GetProduto(id);
            if(produto is null)
            {
               return NotFound("Produto não encontrado.");
            }
            return Ok(produto);
        }

        [HttpPost]
        public ActionResult Post([FromBody] Produto produto)
        {
            if (produto is null)
            {
                return BadRequest("Produto não encontrado");
            }

            _produtoRepository.Create(produto);

            return new CreatedAtRouteResult("ObterProduto", new { id = produto.IdProduto }, produto);
        }

        [HttpPut("{id:int}")]
        public ActionResult Put(int id, [FromBody] Produto produto)
        {
            if (id != produto.IdProduto)
            {
                return BadRequest("Produto não encontrado");
            }

            bool produtoAtualizado = _produtoRepository.Update(produto);

            if (produtoAtualizado)
            {
                return Ok(produto);
            }
            else
            {
                return StatusCode(500, $"Falha ao atualizar produdo de id = {id}");
            }
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            bool produtoDeletado = _produtoRepository.Delete(id);
            if (produtoDeletado)
            {
                return Ok($"Produto excluído.");
            }
            else
            {
               return StatusCode(500, $"Falha ao excluir produto de id = {id}");
            }
        }
    }
}

