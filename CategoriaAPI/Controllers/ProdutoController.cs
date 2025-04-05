using CategoriaAPI.Models;
using CategoriaAPI.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace CategoriaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProdutoController : Controller
    {
        private readonly IProdutoRepository _repository;
        private readonly IMemoryCache _cache;
        private const string CacheProdutosKey = "CacheProdutos";
        public ProdutoController(IProdutoRepository repository, IMemoryCache cache)
        {
            _repository = repository;
            _cache = cache;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Produto>> Get(string? titulo, int? codigoCategoria)
        {
            if (!_cache.TryGetValue(CacheProdutosKey, out IEnumerable<Produto>? itens))
            {
                itens = _repository.GetProdutos(titulo, codigoCategoria).ToList();

                if (itens.Count() != 0)
                {
                    var cacheOptions = new MemoryCacheEntryOptions
                    {
                        AbsoluteExpirationRelativeToNow = TimeSpan.FromDays(10),
                        SlidingExpiration = TimeSpan.FromDays(10),
                        Priority = CacheItemPriority.High
                    };
                    _cache.Set(CacheProdutosKey, itens, cacheOptions);
                }
                else
                {
                    return NotFound();
                }
            }

            return Ok(itens);
        }

        [HttpPost]
        public ActionResult<Produto> Post(Produto produto)
        {
            var created = _repository.Create(produto);
            return Ok(created);
        }

        [HttpPatch("{codigoProduto}")]
        public IActionResult Patch(int codigoProduto, [FromBody] Produto produtoAtualizado)
        {
            var prodtuoExistente = _repository.Update(codigoProduto, produtoAtualizado);

            return Ok(prodtuoExistente);
        }

        [HttpDelete("{codigoProduto}")]
        public IActionResult Delete(int codigoProduto)
        {
            var produto = _repository.Detail(codigoProduto);

            if (produto == null)
                return NotFound();

            _repository.Delete(codigoProduto);
            return NoContent();
        }


        [HttpGet("{codigoProduto}")]
        public ActionResult<Produto> Detail(int codigoProduto)
        {
            var detail = _repository.Detail(codigoProduto);

            if(detail == null)
                return NotFound();

            return Ok(detail);
        }
    }
}
