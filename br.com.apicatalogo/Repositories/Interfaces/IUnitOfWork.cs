using System;
namespace br.com.apicatalogo.Repositories.Interfaces
{
	public interface IUnitOfWork
	{
		IProdutoRepository ProdutoRepository { get; }
		ICategoriaRepository CategoriaRepository { get; }
		void SalvarAlteracoes();
	}
}

