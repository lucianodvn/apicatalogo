using System;
using br.com.apicatalogo.Context;
using br.com.apicatalogo.Models;

namespace br.com.apicatalogo.Repositories
{
    public class ProdutoRepository : IProdutoRepository
	{
        private readonly ApiCatalogoContext _produtoRepository;

        public ProdutoRepository(ApiCatalogoContext context)
		{
            _produtoRepository = context;
		}

        public Produto Create(Produto produto)
        {
            if (produto is null)
            {
                throw new InvalidOperationException("produto é null");
            }

            _produtoRepository.Produtos.Add(produto);
            _produtoRepository.SaveChanges();

            return produto;
        }

        public bool Delete(int id)
        {
            var produto = _produtoRepository.Produtos.Find(id);

            if(produto is not null)
            {
                _produtoRepository.Produtos.Remove(produto);
                _produtoRepository.SaveChanges();
                return true;
            }

            return false;
        }

        public Produto GetProduto(int id)
        {
            var produto = _produtoRepository.Produtos.FirstOrDefault(x => x.IdProduto == id);
            if (produto is null)
            {
                throw new InvalidOperationException("produto é null");
            }

            return produto;
        }

        public IQueryable<Produto> GetProdutos()
        {
            return _produtoRepository.Produtos;
        }

        public bool Update(Produto produto)
        {
            if (produto is null)
            {
                throw new InvalidOperationException("produto é null");
            }

            if (_produtoRepository.Produtos.Any(x => x.IdProduto == produto.IdProduto))
            {
                _produtoRepository.Produtos.Update(produto);
                _produtoRepository.SaveChanges();
                return true;
            }

            return false;
        }
    }
}

