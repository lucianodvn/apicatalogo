using System;
using br.com.apicatalogo.Context;
using br.com.apicatalogo.Models;

namespace br.com.apicatalogo.Repositories
{
    public class ProdutoRepository : Repository<Produto>, IProdutoRepository
	{
        public ProdutoRepository(ApiCatalogoContext context) : base(context)
		{
		}

        public IEnumerable<Produto> GetProdutosPorCategoria(int id)
        {
            return GetAll().Where(c => c.CategoriaId == id);
        }
    }
}

