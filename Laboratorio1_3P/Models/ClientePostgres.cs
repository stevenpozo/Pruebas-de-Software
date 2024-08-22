using System.ComponentModel.DataAnnotations;

namespace Laboratorio1_3P.Models
{
    public class ClientePostgres
    {
        public int codigo { get; set; }

        [Required(ErrorMessage = "El campo cédula es obligatorio.")]
        public string cedula { get; set; }

        [Required(ErrorMessage = "El campo apellidos es obligatorio.")]
        public string apellidos { get; set; }

        [Required(ErrorMessage = "El campo nombres es obligatorio.")]
        public string nombres { get; set; }

        [Required(ErrorMessage = "El campo fecha de nacimiento es obligatorio.")]
        public DateTime fechanacimiento { get; set; }

        [Required(ErrorMessage = "El campo correo es obligatorio.")]
        [EmailAddress(ErrorMessage = "Formato de correo no válido.")]
        public string mail { get; set; }

        [Required(ErrorMessage = "El campo teléfono es obligatorio.")]
        public string telefono { get; set; }

        [Required(ErrorMessage = "El campo dirección es obligatorio.")]
        public string direccion { get; set; }

        public bool estado { get; set; }
    }
}
