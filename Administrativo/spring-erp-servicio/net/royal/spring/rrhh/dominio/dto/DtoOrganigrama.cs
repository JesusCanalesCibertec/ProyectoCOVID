using System;
using System.Collections.Generic;
using System.Text;

namespace net.royal.spring.rrhh.dominio.dto
{
    public class DtoOrganigrama
    {

        public String CompaniaSocio { get; set; }
        public int Anio { get; set; }
        public String UnidadNegocio { get; set; }
        public int Secuencia { get; set; }
        public String Tipo { get; set; }
        public int Codigo { get; set; }
        public int CodigoPadre { get; set; }
        public String Orden { get; set; }
        public String Descripcion { get; set; }
        public String Principal { get; set; }
        public int Nivel { get; set; }
        public int Vacantes { get; set; }
        public int VacantesOcupados { get; set; }
        public int VacantesDisponibles { get; set; }
        public String Icono { get; set; }
        public String DescripcionPadre { get; set; }
        public Int32 CantidadUbicacion { get; set; }
        public String OrganigramaNombre { get; set; }
        public String CodigoNombre { get; set; }


    }
}
