using System;
using System.Collections.Generic;
using System.Text;

namespace net.royal.spring.rrhh.dominio.dto
{
    public class DtoPublicacion
    {
        public Nullable<Int32> idPublicacion { get; set; }
        public String idAplicacion { get; set; }
        public String aplicacionDescripcion { get; set; }
        public String tipoNombre { get; set; }
        public String seguridad { get; set; }
        public String estado { get; set; }
        public Nullable<DateTime> fechaInicio { get; set; }
        public Nullable<DateTime> fechaFin { get; set; }
        public String descripcion { get; set; }

    }
}
