using System;
using br.com.apicatalogo.Context;
using br.com.apicatalogo.Repositories.Interfaces;

namespace br.com.apicatalogo.Repositories
{
	public class UnitOfWork : IUnitOfWork
	{
        public IProdutoRepository? _produtoRepository;

        public ICategoriaRepository? _categoriaRepository;

        public ApiCatalogoContext _context;

        public UnitOfWork(ApiCatalogoContext context)
		{
            _context = context;
		}

        public IProdutoRepository ProdutoRepository
        {
            get
            {
                return _produtoRepository = _produtoRepository ?? new ProdutoRepository(_context);
            }
        } 

        public ICategoriaRepository CategoriaRepository
        {
            get
            {
                return _categoriaRepository = _categoriaRepository ?? new CategoriaRepository(_context);
            }
        }

        public void SalvarAlteracoes()
        {
            _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}

