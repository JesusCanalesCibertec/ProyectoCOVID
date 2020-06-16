using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace net.royal.spring.proyecto.dominio.dto
{
    public class DtoInterfazNotificacion
    {

        public String titulo { get; set; }
        public String descripcion { get; set; }
        public String procesoId { get; set; }
        public String procesoAccion { get; set; }
        public String procesoParametros { get; set; }
        public Int32 procesoIdTransaccion { get; set; }
        public String fuente { get; set; }
        public List<DtoTransaccionUsuario> listaUsuarios { get; set; }

    }

    public class DtoTransaccionUsuario
    {
        public String idRol { get; set; }
        public String idTipoSeguridad { get; set; }
        public Int32 idPersona { get; set; }
        public String nombrePersona { get; set; }
        public String usuarioCodigo { get; set; }
        public String puesto { get; set; }
        public String nombreCentroCosto { get; set; }
        public String nombreSucursal { get; set; }
        public String sucursal { get; set; }
        public String centroCosto { get; set; }

        public String correoInterno { get; set; }
        public String correoExterno { get; set; }
        public String correoElectronico { get; set; }
    }
}
