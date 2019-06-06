using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiPublisher.Model;

namespace WebApiPublisher.Connection
{
    public class ConnContext : DbContext
    {

        public ConnContext()
        {

        }

        public DbSet<Pagina> Pagina { get; set; }
        public DbSet<Parametro> Parametro { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=localhost;Database=Mensageria;Trusted_Connection=True;");
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //base.OnModelCreating(modelBuilder);

            //modelBuilder.Entity<Parametro>()
            //            .HasMany(p => p.Pagina)
            //            .WithOne(par => par.);

            modelBuilder.Entity<Pagina>()
                .HasMany(p => p.Parametros)
                .WithOne(par => par.Pagina);
        }
    }
}
