using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using br.com.apicatalogo.Context;
using br.com.apicatalogo.Models;
using br.com.apicatalogo.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace br.com.apicatalogo.Controllers
{
    [Route("[controller]")]
    public class CategoriasController : ControllerBase
    {

        private readonly ICategoriaRepository _categoriaRepository;

        public CategoriasController(ICategoriaRepository categoriaRepository)
        {
            _categoriaRepository = categoriaRepository;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Categoria>> Get()
        {
            var categorias = _categoriaRepository.GetCategorias();
            if (categorias is null)
            {
                return NotFound("Não há produtos.");
            }
            return Ok(categorias);
        }


        [HttpGet("{id:int}", Name = "ObterCategoria")]
        public ActionResult<Categoria> Get(int id)
        {
            var categoria = _categoriaRepository.GetCategoria(id);
            if (categoria is null)
            {
                return NotFound("Categoria não encontrado.");
            }
            return Ok(categoria);
        }

        [HttpPost]
        public ActionResult Post([FromBody] Categoria categoria)
        {
            if (categoria is null)
            {
                return BadRequest();
            }

            _categoriaRepository.Create(categoria);

            return new CreatedAtRouteResult("ObterCategoria", new { id = categoria.CategoriaId }, categoria);
        }

        [HttpPut("{id:int}")]
        public ActionResult Put(int id, [FromBody] Categoria categoria)
        {
            if (id != categoria.CategoriaId)
            {
                return BadRequest();
            }

            _categoriaRepository.Update(categoria);
  
            return Ok(categoria);
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            Categoria categoriaEspecifico = _categoriaRepository.GetCategoria(id);

            if (categoriaEspecifico is null)
            {
                return NotFound($"Categoria não encontrada. Id={id}");
            }

            _categoriaRepository.Delete(id);
           
            return Ok(categoriaEspecifico);
        }
    }
}

