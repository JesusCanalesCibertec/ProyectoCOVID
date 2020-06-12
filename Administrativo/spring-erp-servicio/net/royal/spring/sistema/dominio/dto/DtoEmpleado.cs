using System;
using System.Collections.Generic;
using System.Text;

namespace net.royal.spring.sistema.dominio.dto
{
    public class DtoEmpleado
    {
        public String NombreCompleto { get; set; }
        public Int32? JefeResponsable { get; set; }
        public String CorreoInterno { get; set; }
        public String UnidadOperativa { get; set; }
        public String DepartamentoOperacional { get; set; }
        public String CentroCostos { get; set; }
        public String DeptoOrganizacion { get; set; }
        public String Compania { get; set; }
        public String CodigoUsuario { get; set; }
        public Int32 empleado { get; set; }
    }
}
