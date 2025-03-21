using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace gestor_de_tareas_personales_practica.Models
{
    public class User
    {
        [Key] // Marca esta propiedad como clave primaria
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // Indica que es autoincremental
        public int Id_User { get; set; }

        [Required]
        [MaxLength(100)] // Longitud máxima del campo Username
        public string Username { get; set; }

        [Required]
        [MaxLength(256)] // Longitud máxima del campo PasswordHash
        public string PasswordHash { get; set; }
    }
}
