using CategoriaAPI.Context;
using CategoriaAPI.Repositories;
using Microsoft.EntityFrameworkCore;

namespace CategoriaTesteApi.UnitTests
{
    public class ProdutosUnitTestController
    {
        public IProdutoRepository repository;
        public static DbContextOptions<CategoriaAPIContext> dbContextOptions { get; }

        public static string connectionString = "Server=localhost;DataBase=CategoriaCrud;Uid=root;Pwd=121416";

        static ProdutosUnitTestController()
        {
            dbContextOptions = new DbContextOptionsBuilder<CategoriaAPIContext>()
                .UseMySql(connectionString, ServerVersion.AutoDetect(connectionString))
                .Options;
        }

        public ProdutosUnitTestController()
        {
            var context = new CategoriaAPIContext(dbContextOptions);
            repository = new ProdutoRepository(context);
        }
    }
}