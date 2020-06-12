using System;
using System.Collections.Generic;
using System.Text;

namespace net.royal.spring.sistema.dominio.dto

{
    public class DtoCorreo
    {
        public DtoCorreo(Int32 empleado, String compania, String correo)
        {
            this.empleado = empleado;
            this.compania = compania;
            this.correo = correo;
        }
        public Int32 empleado { get; set; }
        public String compania { get; set; }
        public String correo { get; set; }
    }
}
