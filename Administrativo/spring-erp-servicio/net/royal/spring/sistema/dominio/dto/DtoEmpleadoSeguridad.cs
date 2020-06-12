using System;
using System.Collections.Generic;
using System.Text;

namespace net.royal.spring.sistema.dominio.dto
{
    public class DtoEmpleadoSeguridad
    {
        public Int32? IdEmpleado { get; set; }
        public String IdEstado { get; set; }
        public String IdEstadoEmpleado { get; set; }
        public String CodigoUsuario { get; set; }
        public String IdCompaniaSocio { get; set; }
    }
}
