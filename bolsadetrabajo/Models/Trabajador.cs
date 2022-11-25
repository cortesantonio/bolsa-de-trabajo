using System.ComponentModel.DataAnnotations;

namespace bolsadetrabajo.Models
{
    public class Trabajador
    {
        [Key]
        public int idTrabajador { get; set; }

        public string? Nombre { get; set; }
        public string? Rut { get; set; }
        public string? FechaNacimiento { get; set; }
        public string? Sexo { get; set; }
        public string? NumeroContacto { get; set; }
        public string? Direccion { get; set; }
        public string? Profesion { get; set; }

        public string Email { get; set; }
        public byte[]? PasswordHash { get; set; }
        public byte[]? PasswordSalt { get; set; }




        



    }


}
