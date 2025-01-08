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

    [HttpDelete]
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
