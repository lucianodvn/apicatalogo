using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using br.com.apicatalogo.Context;
using br.com.apicatalogo.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace br.com.apicatalogo.Controllers
{
    [Route("[controller]")]
    public class ProdutosController : ControllerBase
    {
        private readonly ApiCatalogoContext _context;

        public ProdutosController(ApiCatalogoContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Produto>> Get()
        {
            var produtos = _context.Produtos.ToList();
            if (produtos is null)
            {
                return NotFound("Não há produtos.");
            }
            return produtos;
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}

