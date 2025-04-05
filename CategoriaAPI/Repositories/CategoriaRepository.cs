using CategoriaAPI.Context;
using CategoriaAPI.Models;

namespace CategoriaAPI.Repositories
{
    public class CategoriaRepository : ICategoriaRepository
    {
        private readonly CategoriaAPIContext _context;

        public CategoriaRepository(CategoriaAPIContext context)
        {
            _context = context;
        }

        public IEnumerable<Categoria> GetCategorias(string? titulo)
        {
            var itens = _context.Categoria.ToList();

            if (titulo != null)
            {
                itens = itens.Where(a => a.Titulo.ToUpper().Contains(titulo.ToUpper())).ToList();
            }

            return itens;
        }

        public Categoria Create(Categoria categoria)
        {
            var createdPedido = _context.Categoria.Add(categoria);
            _context.SaveChanges();
            return categoria;
        }

        public Categoria Update(int codigoCategoria, Categoria categoriaAtualizada)
        {
            var categoriaExistente = _context.Categoria.FirstOrDefault(c => c.Codigo == codigoCategoria);

            categoriaExistente.Titulo = categoriaAtualizada.Titulo ?? categoriaExistente.Titulo;
            categoriaExistente.Descricao = categoriaAtualizada.Descricao ?? categoriaExistente.Descricao;

            _context.SaveChanges();

            return categoriaExistente;
        }

        public void Delete(int codigoCategoria)
        {
            var categoria = _context.Categoria.FirstOrDefault(a => a.Codigo == codigoCategoria);

            if(categoria != null)
            {
                _context.Remove(categoria);
            }

            _context.SaveChanges();
        }
    }
}
