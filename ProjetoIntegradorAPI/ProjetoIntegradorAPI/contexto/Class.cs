using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ProjetoIntegradorAPI.Model;

namespace ProjetoIntegradorAPI.contexto
{
    public class AppDbContext :IdentityDbContext
    {

        public AppDbContext(DbContextOptions<AppDbContext> options): base (options)
        {

        }
        public DbSet<Coordenadas> Coordenadas{ get; set; }
        public AppDbContext() { }
    }
}
