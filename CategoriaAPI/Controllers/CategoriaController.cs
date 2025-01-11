using CategoriaAPI.Context;
using CategoriaAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;

namespace CategoriasAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CategoriaController : ControllerBase
{
    private readonly CategoriaAPIContext _context;
    public CategoriaController(CategoriaAPIContext context)
    {
        _context = context;
    }

    [HttpGet]
    public ActionResult<IEnumerable<Categoria>> Get(string? titulo)
    {
        var itens = _context.Categoria.ToList();

        if (titulo != null)
        {
            itens = itens.Where(a => a.Titulo.ToUpper().Contains(titulo.ToUpper())).ToList();
        }
        if (itens is null)
        {
            return NotFound();
        }
        return itens;
    }

    [HttpPost]
    public ActionResult<Categoria> Post([FromBody] Categoria categoria)
    {
        var createdPedido = _context.Categoria.Add(categoria);
        _context.SaveChanges();
        return categoria;
    }

    [HttpPatch("{codigoCategoria}")]
    public IActionResult Patch(int codigoCategoria, [FromBody] Categoria categoriaAtualizada)
    {
        var categoriaExistente = _context.Categoria.FirstOrDefault(c => c.Codigo == codigoCategoria);

        if (categoriaExistente == null)
        {
            return NotFound(new { mensagem = "Categoria não encontrada." });
        }

        categoriaExistente.Titulo = categoriaAtualizada.Titulo ?? categoriaExistente.Titulo;
        categoriaExistente.Descricao = categoriaAtualizada.Descricao ?? categoriaExistente.Descricao;

        try
        {
            _context.Entry(categoriaExistente).State = EntityState.Modified;
            _context.SaveChanges();
        }
        catch (DbUpdateException ex)
        {
            return BadRequest(new { mensagem = "Erro ao atualizar a categoria.", detalhes = ex.Message });
        }

        return Ok(categoriaExistente);
    }


    [HttpDelete("{codigoCategoria}")]
    public void Delete(int codigoCategoria)
    {
        var categoria = _context.Categoria.FirstOrDefault(a => a.Codigo == codigoCategoria);
        if (categoria != null)
        {
            _context.Categoria.Remove(categoria);
        }
        _context.SaveChanges(); ;
    }
}
