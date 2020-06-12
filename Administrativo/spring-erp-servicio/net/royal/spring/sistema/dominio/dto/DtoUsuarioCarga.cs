using System;
using System.Collections.Generic;
using System.Text;

namespace net.royal.spring.sistema.dominio.dto
{
    public class DtoUsuarioCarga
    {
        public String Respuesta { get; set; }
        public String Accion { get; set; }
        public String Compania { get; set; }
        public Int32? IdPersona { get; set; }
        public String Nombres { get; set; }
        public String ApellidoPaterno { get; set; }
        public String ApellidoMaterno { get; set; }
        public String NumeroDocumento { get; set; }
        public String Usuario { get; set; }
        public String Clave { get; set; }
        public String Perfil { get; set; }
        public String CorreoCorporativo { get; set; }
        public String CorreoInterno { get; set; }
        public String Configuracion { get; set; }
    }
}
