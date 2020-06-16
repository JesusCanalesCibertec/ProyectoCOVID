using System;
using System.Collections.Generic;
using System.Text;

namespace net.royal.spring.minedu.dominio.dto
{
    public class DtoProyecto
    {
        public String Tipoproyecto { get; set; }
        public String Nomtipoproyecto { get; set; }
        public Int32? Idproyecto { get; set; }
        public String Nombre { get; set; }
        public Int32? Area { get; set; }
        public String Niveles { get; set; }
        public DateTime? Fechainicio { get; set; }
        public DateTime? Fechafin { get; set; }
        public String Estado { get; set; }
        public String Nomestado { get; set; }
        public Decimal? Desviacion { get; set; }
        public Decimal? planificado { get; set; }
        public Decimal? real { get; set; }
      
        public String Gestor { get; set; }
        public String Asunto { get; set; }
        public String NomArea { get; set; }
        public String Observacion { get; set; }
        public Int32? Secuencia { get; set; }
        public String Expediente { get; set; }
        public DateTime? Fechaexpediente { get; set; }
        public String Proceso { get; set; }


        //no mapeados
        public Int32? idSIS { get; set; }
        public String NombreSIS { get; set; }
        public String NomcortoSIS { get; set; }

    }
}
