using MapaCidade.Dominio.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapaCidade.Infra.Bd.Config
{
    public class MapaContext : DbContext
    {
        public DbSet<EstacaoRecarga> EstacaoRecargas { get; set; }

        public MapaContext()
        {
                
        }
        public MapaContext(DbContextOptions<MapaContext> options) : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(GetConnectionString());

            }
        }

        private string GetConnectionString()
        {
            return @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Elias\source\repos\MapaCidade\MapaCidade.Infra.Bd\Bd\DatabaseMapa.mdf;Integrated Security=True";
        }
    }
}
