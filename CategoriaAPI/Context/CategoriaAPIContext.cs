using Microsoft.EntityFrameworkCore;
using CategoriaAPI.Models;

namespace CategoriaAPI.Context;

public class CategoriaAPIContext : DbContext
{
    public CategoriaAPIContext(DbContextOptions<CategoriaAPIContext> options) : base(options)
    {
    }

    public DbSet<Categoria> Categoria { get; set; }
}
