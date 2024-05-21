using System;
using br.com.apicatalogo.Context;
using br.com.apicatalogo.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace br.com.apicatalogo.Repositories
{
	public class CategoriaRepository : ICategoriaRepository
	{
        private readonly ApiCatalogoContext _produtoRepository;

        public CategoriaRepository(ApiCatalogoContext context)
		{
            _produtoRepository = context;
		}

        public Categoria Create(Categoria categoria)
        {
            if (categoria is null)
            {
                throw new InvalidOperationException("Categorias é null");
            }

            _produtoRepository.Categorias.Add(categoria);
            _produtoRepository.SaveChanges();
            return categoria;
        }

        public Categoria Delete(int id)
        {
            Categoria categoriaEspecifico = _produtoRepository.Categorias.Find(id);

            if (categoriaEspecifico is null)
            {
                throw new InvalidOperationException($"Categoria não encontrada. Id={id}");
            }

            _produtoRepository.Categorias.Remove(categoriaEspecifico);
            _produtoRepository.SaveChanges();
            return categoriaEspecifico;
        }

        public Categoria GetCategoria(int id)
        {
            var categoria = _produtoRepository.Categorias.FirstOrDefault(x => x.CategoriaId == id);
            if (categoria is null)
            {
                throw new InvalidOperationException("Categorias é null");
            }
            return categoria;
        }

        public IEnumerable<Categoria> GetCategorias()
        {
            var categorias = _produtoRepository.Categorias.ToList();
            if (categorias is null)
            {
                throw new InvalidOperationException("Categorias é null");
            }
            return categorias;
        }

        public Categoria Update(Categoria categoria)
        {
            if (categoria is null)
            {
                throw new InvalidOperationException("Categorias é null");
            }

            _produtoRepository.Categorias.Entry(categoria).State = EntityState.Modified;
            _produtoRepository.SaveChanges();
            return categoria;
        }
    }
}

