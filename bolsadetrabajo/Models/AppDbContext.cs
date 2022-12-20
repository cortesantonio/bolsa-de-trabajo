using Microsoft.EntityFrameworkCore;

namespace bolsadetrabajo.Models
{
    public class AppDbContext : DbContext
    {
        public DbSet<Empresa> Empresa { get; set; }
        public DbSet<Trabajador> Trabajador { get; set; }
        public DbSet<Publicacion> Publicacion { get; set; }
        public DbSet<PublicacionGuardada> PublicacionGuardada { get; set; }
        public DbSet<NumeroContactoPublicacion> NumeroContactoPublicacions { get; set; }
        public DbSet<VisitasPublicacion> VisitasPublicacion { get; set; }



        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=bolsa;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
        }

    }
}