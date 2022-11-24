namespace bolsadetrabajo.Models
{
    public class TrabajadorViewModel
    {
        public int idTrabajador { get; set; }
        public string Nombre { get; set; }
        public string Rut { get; set; }
        public string? FechaNacimiento { get; set; }
        public string? Sexo { get; set; }
        public string? NumeroContacto { get; set; }
        public string? Direccion { get; set; }
        public string? Profesion { get; set; }

        public string Email { get; set; }
        public string? Password { get; set; }
        public byte[]? PasswordHash { get; set; }
        public byte[]? PasswordSalt { get; set; }

    }
}