using System;
using br.com.apicatalogo.Models;

namespace br.com.apicatalogo.Repositories
{
	public interface IProdutoRepository : IRepository<Produto>
	{
		IEnumerable<Produto> GetProdutosPorCategoria(int id);
	}
}

