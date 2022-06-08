using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InmoNovara.Models
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }
        
        public DbSet<Propietario> Propietarios { get; set; }
        public DbSet<Inquilino> Inquilino { get; set; }
        public DbSet<Inmueble> Inmueble { get; set; }
        public DbSet<Usuario> Usuario { get; set; }
        public DbSet<Contrato> Contrato { get; set; }
        public DbSet<Garante> Garante { get; set; }
        public DbSet<Pago> Pago { get; set; }

    }
}
