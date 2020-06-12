using System;
using System.Collections.Generic;
using System.Text;

namespace net.royal.spring.rrhh.dominio.dto
{
    public class DtoPsBeneficiario
    {
        public String idInstitucion { get; set; }
		public String idPrograma { get; set; }
		public String auxInstitucion { get; set; }

        public Nullable<Int32> idBeneficiario { get; set; }
        public String nombre { get; set; }
        public String documento { get; set; }

        public Nullable<Int32> edad { get; set; }
        public String sexo { get; set; }


    }
}
