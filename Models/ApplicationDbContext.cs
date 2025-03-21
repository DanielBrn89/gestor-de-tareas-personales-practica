using Microsoft.EntityFrameworkCore;

namespace gestor_de_tareas_personales_practica.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configurar la tabla y la clave primaria
            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("Users"); // Nombre de la tabla en la base de datos
                entity.HasKey(u => u.Id_User); // Clave primaria
                entity.Property(u => u.Id_User).ValueGeneratedOnAdd(); // Autoincremental
                entity.Property(u => u.Username).HasMaxLength(100).IsRequired(); // Longitud y requerido
                entity.Property(u => u.PasswordHash).HasMaxLength(256).IsRequired(); // Longitud y requerido
            });
        }
    }
}
