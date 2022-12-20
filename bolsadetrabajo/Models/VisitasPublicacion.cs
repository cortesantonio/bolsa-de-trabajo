using System.ComponentModel.DataAnnotations;

namespace bolsadetrabajo.Models
{
    public class VisitasPublicacion
    {

        [Key]
        public int Id { get; set; }
        public int TrabajadorId { get; set; }
        public Trabajador? Trabajador { get; set; }
        public int PublicacionId { get; set; }
        public Publicacion? Publicacion { get; set; }
    }
        
}
