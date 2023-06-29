using clientMqtt.Model;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace clientMqtt.Context
{
    public class Contexto : DbContext
    {
        public DbSet<Coordenadas> Coordenadas { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var connectionString = "Data Source=localhost;initial catalog=testeLogin;persist security info=True;user id=sa;password=ACESSO@sql;multipleactiveresultsets=True;application name=EntityFramework&quot;Encrypt=False";

                optionsBuilder.UseSqlServer(connectionString);
            }
        }
    }
}
