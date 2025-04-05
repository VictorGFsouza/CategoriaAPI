using System.ComponentModel.DataAnnotations;

namespace CategoriaAPI.Models
{
    public class Produto
    {
        [Key]
        public int Codigo { get; set; }

        public required string Nome { get; set; }

        public required decimal Valor { get; set; }

        public string? Descricao { get; set; }

        public required int CodigoCategoria { get; set; }
    }
}
