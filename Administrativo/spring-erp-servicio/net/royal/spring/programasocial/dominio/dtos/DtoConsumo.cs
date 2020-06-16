using System;
using System.Collections.Generic;
using System.Text;

namespace net.royal.spring.rrhh.dominio.dto
{
    public class DtoConsumo
    {
        public Int32? codConsumo { get; set; }
        public String codInstitucion{ get; set; }
        public String nomInstitucion{ get; set; }
        public String codPeriodo { get; set; }
        public Nullable<DateTime> fechainicio { get; set; }
        public Nullable<DateTime> fechafin { get; set; }
        public String descripcion { get; set; }
        public String estado { get; set; }
        public String aportante { get; set; }


    }
    public class DtoConsumoCarga
    {
        public String excel { get; set; }
    }
}
