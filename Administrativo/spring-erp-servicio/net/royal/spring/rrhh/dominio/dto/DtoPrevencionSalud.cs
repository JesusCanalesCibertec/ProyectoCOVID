using net.royal.spring.programasocial.dominio;
using System;
using System.Collections.Generic;
using System.Text;

namespace net.royal.spring.rrhh.dominio.dto
{
    public class DtoPrevencionSalud
    {


        public String idInstitucion { get; set; }
        public Nullable<Int32> idBeneficiario { get; set; }
        public String apePaterno { get; set; }
        public String apeMaterno { get; set; }
        public String nombres { get; set; }
        public Nullable<DateTime> fecha { get; set; }
        public Nullable<Int32> medicina { get; set; }
        public Nullable<Int32> odontologia { get; set; }
        public Nullable<Int32> psicologia{ get; set; }
        public Nullable<Int32> terapia { get; set; }
        public Nullable<Int32> nutricion { get; set; }
        public Nullable<Int32> otrosTaller { get; set; }
        public Nullable<Int32> vacunas { get; set; }
        public Nullable<Int32> profilaxis { get; set; }
        public Nullable<Int32> diagnostico { get; set; }
        public Nullable<Int32> tratamiento { get; set; }
        public Nullable<Int32> otrosCampanias { get; set; }


        public String codPeriodo { get; set; }
        public String nomComponente { get; set; }




    }
}
