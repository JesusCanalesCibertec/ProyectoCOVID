using System;
using System.Collections.Generic;
using System.Text;

namespace net.royal.spring.programasocial.dominio.dtos {
    public class DtoAtencion {

        public String IdInstitucion { get; set; }
        public Nullable<Int32> IdBeneficiario { get; set; }
        public String NombreCompleto { get; set; }
        public String Documento { get; set; }
        public String IdTipoAtencion { get; set; }
        public String IdArea { get; set; }
        public Nullable<Int32> IdAtencion { get; set; }
        public String idPeriodo { get; set; }
        public Nullable<DateTime> FechaAtencion { get; set; }

        public String Residencia { get; set; }

        public Nullable<Int32> IdDiagnostico01 { get; set; }
        public Nullable<Int32> IdDiagnostico02 { get; set; }
        public Nullable<Int32> IdDiagnostico03 { get; set; }
        public Nullable<Int32> IdDiagnostico04 { get; set; }
        public Nullable<Int32> IdDiagnostico05 { get; set; }

        public String NombreDiagnostico01 { get; set; }
        public String NombreDiagnostico02 { get; set; }
        public String NombreDiagnostico03 { get; set; }
        public String NombreDiagnostico04 { get; set; }
        public String NombreDiagnostico05 { get; set; }


        public String IdTratamiento01_1 { get; set; }
        public String IdTratamiento01_2 { get; set; }
        public String IdTratamiento01_3 { get; set; }

        public String IdTratamiento02_1 { get; set; }
        public String IdTratamiento02_2 { get; set; }
        public String IdTratamiento02_3 { get; set; }

        public String IdTratamiento03_1 { get; set; }
        public String IdTratamiento03_2 { get; set; }
        public String IdTratamiento03_3 { get; set; }

        public String IdTratamiento04_1 { get; set; }
        public String IdTratamiento04_2 { get; set; }
        public String IdTratamiento04_3 { get; set; }

        public String IdTratamiento05_1 { get; set; }
        public String IdTratamiento05_2 { get; set; }
        public String IdTratamiento05_3 { get; set; }


        public String IdTratamiento01_1_nombre { get; set; }
        public String IdTratamiento01_2_nombre { get; set; }
        public String IdTratamiento01_3_nombre { get; set; }

        public String IdTratamiento02_1_nombre { get; set; }
        public String IdTratamiento02_2_nombre { get; set; }
        public String IdTratamiento02_3_nombre { get; set; }

        public String IdTratamiento03_1_nombre { get; set; }
        public String IdTratamiento03_2_nombre { get; set; }
        public String IdTratamiento03_3_nombre { get; set; }

        public String IdTratamiento04_1_nombre { get; set; }
        public String IdTratamiento04_2_nombre { get; set; }
        public String IdTratamiento04_3_nombre { get; set; }

        public String IdTratamiento05_1_nombre { get; set; }
        public String IdTratamiento05_2_nombre { get; set; }
        public String IdTratamiento05_3_nombre { get; set; }

        public String comentario { get; set; }

        public Nullable<Int32> Posicion { get; set; }

       

    }
}
