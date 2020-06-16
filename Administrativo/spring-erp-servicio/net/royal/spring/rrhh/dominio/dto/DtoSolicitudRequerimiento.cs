using System;
using System.Collections.Generic;
using System.Text;

namespace net.royal.spring.rrhh.dominio.dto
{
    public class DtoSolicitudRequerimiento
    {
        public String Requerimiento { get; set; }
        public String Companyowner { get; set; }
        public Nullable<Int32> PuestoId { get; set; }
        public String PuestoNombre { get; set; }
        public Nullable<DateTime> FechaSolicitud { get; set; }
        public Nullable<Int32> EmpleadoSolicitanteId { get; set; }
        public String EmpleadoSolicitanteNombre { get; set; }
        public String EstadoId { get; set; }
        public String EstadoNombre { get; set; }

        public Nullable<Int32> Numerosolicitado { get; set; }
        public String Centrocosto { get; set; }
        public String CentrocostoNombre { get; set; }

        public Boolean PostulanteFlgBloqueado { get; set; }
        public String PostulanteLeyenda { get; set; }
        public String PostulanteImagen { get; set; }
        public Int32 PostulanteIndicador { get; set; }
        public Int32 NumeroPostulantes { get; set; }

        public Nullable<DateTime> VigenciaInicio { get; set; }
        public Nullable<DateTime> VigenciaFin { get; set; }
        public Int32 Secuencia { get; set; }
        public Int32 Secuencia2 { get; set; }
        public Int32 Factor { get; set; }
        public Single ValorCompetencia { get; set; }
        public Single ValorHastaCompetencia { get; set; }
        public String codigoproceso { get; set; }
    }
}
