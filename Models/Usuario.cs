namespace ApiTrivIA.Models
{
    public class Usuario
    {
        public string? uidUsuario { get; set; } // Identificador único del usuario
        public string usuario { get; set; }      // Nombre del usuario
        public string correo { get; set; }      // Correo electrónico del usuario
        public string contrasenia { get; set; } // Contraseña del usuario
        /*public DateTime? audFechaCreacion { get; set; } // Fecha de creación del registro
        public DateTime? audFechaMod { get; set; }     // Fecha de modificación del registro
        public string? audEstadoExistencia { get; set; } // Estado de existencia del usuario (A, E)*/
    }
}