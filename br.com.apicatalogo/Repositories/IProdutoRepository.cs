using System;
using br.com.apicatalogo.Models;

namespace br.com.apicatalogo.Repositories
{
	public interface IProdutoRepository
	{
		IQueryable<Produto> GetProdutos();
		Produto GetProduto(int id);
		Produto Create(Produto produto);
		bool Update(Produto produto);
		bool Delete(int id);
	}
}

