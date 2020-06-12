using System;
using System.Collections.Generic;
using System.Text;

namespace net.royal.spring.rrhh.dominio.dto
{
    public class DtoCurso
    {
        public Nullable<Int32> idCurso{ get; set; }
        public String nombre{ get; set; }
        public String tipoId{ get; set; }
        public String tipoNombre{ get; set; }
        public String areaId{ get; set; }
        public String areaNombre{ get; set; }
        public String estadoId{ get; set; }
        public String estadoNombre{ get; set; }

    }
}
