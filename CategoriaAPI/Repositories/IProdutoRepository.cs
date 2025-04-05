using CategoriaAPI.Models;

namespace CategoriaAPI.Repositories
{
    public interface IProdutoRepository
    {
        IEnumerable<Produto> GetProdutos(string? titulo, int? codigoCategoria);

        Produto Create(Produto produto);

        Produto Update(int codigoProduto, Produto produtoAtualizado);

        void Delete(int codigoProduto);

        Produto Detail(int codigoProduto);
    }
}
