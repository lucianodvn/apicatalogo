using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
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
    public class CategoriasController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public CategoriasController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Categoria>> Get()
        {
            var categorias = _unitOfWork.CategoriaRepository.GetAll();
            if (categorias is null)
            {
                return NotFound("Não há produtos.");
            }
            return Ok(categorias);
        }


        [HttpGet("{id:int}", Name = "ObterCategoria")]
        public ActionResult<Categoria> Get(int id)
        {
            var categoria = _unitOfWork.CategoriaRepository.Get(c => c.CategoriaId == id);
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

            _unitOfWork.CategoriaRepository.Create(categoria);
            _unitOfWork.SalvarAlteracoes();

            return new CreatedAtRouteResult("ObterCategoria", new { id = categoria.CategoriaId }, categoria);
        }

        [HttpPut("{id:int}")]
        public ActionResult Put(int id, [FromBody] Categoria categoria)
        {
            if (id != categoria.CategoriaId)
            {
                return BadRequest();
            }

            _unitOfWork.CategoriaRepository.Update(categoria);
            _unitOfWork.SalvarAlteracoes();

            return Ok(categoria);
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            Categoria categoriaEspecifico = _unitOfWork.CategoriaRepository.Get(c => c.CategoriaId == id);

            if (categoriaEspecifico is null)
            {
                return NotFound($"Categoria não encontrada. Id={id}");
            }

            _unitOfWork.CategoriaRepository.Delete(categoriaEspecifico);
            _unitOfWork.SalvarAlteracoes();

            return Ok(categoriaEspecifico);
        }
    }
}

