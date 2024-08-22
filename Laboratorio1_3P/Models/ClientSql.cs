using System.ComponentModel.DataAnnotations;

namespace Laboratorio1_3P.Models
{
    public class ClientSql
    {
        public int Codigo { get; set; }

        [Required(ErrorMessage = "El campo cédula es obligatorio.")]
        public string Cedula { get; set; }

        [Required(ErrorMessage = "El campo apellidos es obligatorio.")]
        public string Apellidos { get; set; }

        [Required(ErrorMessage = "El campo nombres es obligatorio.")]
        public string Nombres { get; set; }

        [Required(ErrorMessage = "El campo fecha de nacimiento es obligatorio.")]
        public DateTime FechaNacimiento { get; set; }

        [Required(ErrorMessage = "El campo corre o es obligatorio.")]
        [EmailAddress(ErrorMessage = "Formato de correo no válido.")]
        public string Mail { get; set; }

        [Required(ErrorMessage = "El campo teléfono es obligatorio.")]
        public string Telefono { get; set; }

        [Required(ErrorMessage = "El campo dirección es obligatorio.")]
        public string Direccion { get; set; }


        public bool Estado { get; set; }

        [Required(ErrorMessage = "El campo de saldo es obligatorio.")]
        public decimal Saldo { get; set; }

        public string Provincia { get; set; }
    }
}
