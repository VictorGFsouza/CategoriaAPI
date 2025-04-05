using CategoriaAPI.Context;
using CategoriaAPI.Models;
using CategoriaAPI.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;

namespace CategoriasAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CategoriaController : ControllerBase
{
    private readonly ICategoriaRepository _repository;
    public CategoriaController(ICategoriaRepository repository)
    {
        _repository = repository;
    }

    [HttpGet]
    public ActionResult<IEnumerable<Categoria>> Get(string? titulo)
    {
        var itens = _repository.GetCategorias(titulo).ToList();

        if (itens is null)
        {
            return NotFound();
        }

        return Ok(itens);
    }

    [HttpPost]
    public ActionResult<Categoria> Post([FromBody] Categoria categoria)
    {
        var createdPedido = _repository.Create(categoria);
        return Ok(createdPedido);
    }

    [HttpPatch("{codigoCategoria}")]
    public IActionResult Patch(int codigoCategoria, [FromBody] Categoria categoriaAtualizada)
    {
        var categoriaExistente = _repository.Update(codigoCategoria, categoriaAtualizada);

        if (categoriaExistente == null)
        {
            return NotFound(new { mensagem = "Categoria não encontrada." });
        }

        return Ok(categoriaExistente);
    }


    [HttpDelete("{codigoCategoria}")]
    public void Delete(int codigoCategoria)
    {
        _repository.Delete(codigoCategoria);
    }
}
