using System.ComponentModel.DataAnnotations;

namespace CategoriaAPI.Models
{
    public class Categoria
    {
        [Key]
        public int Codigo { get; set; }

        public required string Titulo { get; set; }

        public string? Descricao { get; set; }
    }
}
