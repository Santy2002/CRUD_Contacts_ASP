using CRUD_ASP.Models;
using Microsoft.EntityFrameworkCore;

namespace CRUD_ASP.Datos
{
    public class ApplicationDBContext : DbContext
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> opciones) : base(opciones)
        {

        }

        //Agrega el modelo Usuario y se deberian agregar todos los demas modelos
        public DbSet<Usuario> Usuario { get; set; }
    }
}
