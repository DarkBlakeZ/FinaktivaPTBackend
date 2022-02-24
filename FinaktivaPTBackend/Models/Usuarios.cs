using System;

namespace FinaktivaPT.Models
{
    public class GetUsuarios
    {
        public long Id { get; set; }
        public string Nombre { get; set; }
        public string Username { get; set; }
        public long IdRol { get; set; }
        public string Rol { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime? FechaModificacion { get; set; }
    }

    public class CUUsuarios
    {
        public long Id { get; set; }
        public string Nombre { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public long Rol { get; set; }
    }
    public class respuesta
    {
        public CUUsuarios result { get; set; }
        public string strMensaje { get; set; }
        public bool logRes { get; set; }

    }
}
