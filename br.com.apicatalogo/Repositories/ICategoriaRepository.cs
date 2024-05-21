using System;
using br.com.apicatalogo.Models;

namespace br.com.apicatalogo.Repositories
{
	public interface ICategoriaRepository
	{
		IEnumerable<Categoria> GetCategorias();
		Categoria GetCategoria(int id);
		Categoria Create(Categoria categoria);
		Categoria Update(Categoria categoria);
		Categoria Delete(int id);
	}
}

