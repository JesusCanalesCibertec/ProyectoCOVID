using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace net.royal.spring.proyecto.dominio.dto
{
    public class DtoProyecto
    {
        public Int32 IdPortafolio { get; set; }
        public Int32 IdPrograma { get; set; }
        public Int32 IdProyecto { get; set; }
        public String NombreProyecto { get; set; }
        public String ResponsableNombre { get; set; }
        public String EstadoDescripcion { get; set; }
        public DateTime? FechaInicio { get; set; }
        public DateTime? FechaFin { get; set; }
        public Int32? IdResponsable { get; set; }
        public Int32 IdTransaccion { get; set; }
        public Int32? auxPlantilla { get; set; }

        public Nullable<Decimal>MontoPresupuestado { get; set; }
        public Nullable<Decimal> MontoGastado { get; set; }
        public Nullable<DateTime> FechaFinPresupuestado { get; set; }
        public Nullable<DateTime> FechaFinEstimado { get; set; }

    }
}
