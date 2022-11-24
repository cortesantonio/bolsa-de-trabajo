using System.ComponentModel.DataAnnotations;

namespace bolsadetrabajo.Models
{
    public class Publicacion
    {
        [Key]
        public int idPublicacion { get; set; }
        public string Fecha { get; set; }
        public string Titulo { get; set; }
        public string? Area { get; set; }
        public string? TipoJornada { get; set; }
        public string Ubicacion { get; set; }
        public string Descripcion { get; set; }
        public int? Experiencia { get; set; }
        public int? NumeroVacantes { get; set; }
        public int? Sueldo { get; set; }
        public string? TipoContrato { get; set; }
        public int EmpresaId { get; set; }
        public Empresa? Empresa { get; set; }




    }
}
