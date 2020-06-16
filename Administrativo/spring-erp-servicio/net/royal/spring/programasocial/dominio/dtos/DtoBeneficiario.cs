using System;
using System.Collections.Generic;
using System.Text;

namespace net.royal.spring.rrhh.dominio.dto {
    public class DtoBeneficiario {
        public Int32? secuencia { get; set; }
        public String idInstitucion { get; set; }
        public String institucion { get; set; }
        public Nullable<Int32> idBeneficiario { get; set; }
        public String beneficiario { get; set; }
        public String dni { get; set; }
        public String tipodocumento { get; set; }
        public String sexo { get; set; }
        public Nullable<Int32> edad { get; set; }
        public Nullable<DateTime> fechaReg { get; set; }
        public Int32 tipoNuevo { get; set; }
        public String IdPrograma { get; set; }
        public String estado { get; set; }
        public String IdDiscapacidad { get; set; }
        public Nullable<Int32> IdComponenteSalud { get; set; }
        public Nullable<Int32> IdComponenteCapacidad { get; set; }
        public Nullable<Int32> IdComponenteSocioAmbiental { get; set; }
        public Nullable<Int32> IdComponenteNutricion { get; set; }

        public String IdMotivo { get; set; }


    }
}
