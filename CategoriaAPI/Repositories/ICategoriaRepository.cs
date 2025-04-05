using CategoriaAPI.Models;

namespace CategoriaAPI.Repositories
{
    public interface ICategoriaRepository
    {
        IEnumerable<Categoria> GetCategorias(string? titulo);

        Categoria Create(Categoria categoria);

        Categoria Update(int codigoCategoria, Categoria categoriaAtualizada);

        void Delete(int codigoCategoria);
    }
}
