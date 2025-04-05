using CategoriaAPI.Context;
using CategoriaAPI.Models;

namespace CategoriaAPI.Repositories
{
    public class ProdutoRepository : IProdutoRepository
    {
        private readonly CategoriaAPIContext _context;

        public ProdutoRepository(CategoriaAPIContext context)
        {
            _context = context;
        }

        public IEnumerable<Produto> GetProdutos(string? titulo, int? codigoCategoria)
        {
            var itens = _context.Produto.ToList();

            if (titulo != null)
            {
                itens = itens.Where(a => a.Nome.ToUpper().Contains(titulo.ToUpper())).ToList();
            }

            if(codigoCategoria != null)
                itens = itens.Where(i => i.CodigoCategoria == codigoCategoria).ToList();

            return itens;
        }

        public Produto Create(Produto produto)
        {
            _context.Produto.Add(produto);
            _context.SaveChanges();
            return produto;
        }

        public Produto Update(int codigoProduto, Produto produtoAtualizado)
        {
            _context.SaveChanges();

            return produtoAtualizado;
        }

        public void Delete(int codigoProduto)
        {
            var produto = _context.Produto.FirstOrDefault(a => a.Codigo == codigoProduto);

            if(produto != null)
            {
                _context.Remove(produto);
            }

            _context.SaveChanges();
        }

        public Produto Detail(int codigoProduto)
        {
            var produto = _context.Produto.FirstOrDefault(a => a.Codigo == codigoProduto);

            if (produto is null)
                throw new Exception("Produto não encontrado");

            return produto;
        }
    }
}
