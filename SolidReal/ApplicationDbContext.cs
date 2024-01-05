using Microsoft.EntityFrameworkCore;
using MySolidAPI.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace MySolidAPI
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Tarea>().Property(c => c.Id).ValueGeneratedNever();
            modelBuilder.Entity<Usuario>().Property(c => c.Id).ValueGeneratedNever();
        }

        public DbSet<Tarea> Tareas { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
    }
}
