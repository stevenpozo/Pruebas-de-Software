using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Laboratorio1_3P.Models
{
    public class ClienteSqlPrueba
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ProductId { get; set; }

        [Required(ErrorMessage = "El nombre del producto es obligatorio.")]
        public string ProductName { get; set; }

        [Required(ErrorMessage = "El campo categoría es obligatorio.")]
        public string Category { get; set; }

        [Required(ErrorMessage = "El precio es obligatorio.")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "La cantidad en stock es obligatoria.")]
        public int StockQuantity { get; set; }

    }
}
