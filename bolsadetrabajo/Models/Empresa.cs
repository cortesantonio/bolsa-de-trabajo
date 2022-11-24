using System.ComponentModel.DataAnnotations;

namespace bolsadetrabajo.Models
{
    public class Empresa
    {
        [Key]
        public int idEmpresa { get; set; }
        public string RazonSocial { get; set; }
        public string? Rut { get; set; }
        public string? Direccion { get; set; }
        public string? NumeroContacto { get; set; }
        public string? RutRepresentante { get; set; }
        public string? NombreRepresentante { get; set; }

        public string Email { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }








    }


}
