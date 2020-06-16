using System;
using System.Collections.Generic;
using System.Text;

namespace net.royal.spring.framework.core.dominio.dto
{
    public class DtoVacacionPeriodo
    {
        public Nullable<Int32> empleado{ get; set; }
        public Nullable<Int32> numeroperiodo { get; set; }
        public String ano{ get; set; }
        public String mes{ get; set; }
        public Nullable<Double> Derecho { get; set; }
        public Nullable<Double> diasganados { get; set; }
        public String ultimousuario{ get; set; }
        public Nullable<DateTime> ultimafechamodif { get; set; }
        public Nullable<Double> pendientepagoanterior { get; set; }
        public Nullable<Double> pendientesreales { get; set; }
        public Nullable<Double> pendientesrealizados { get; set; }
        public Nullable<Double> diasprescritos { get; set; }
        public String estado{ get; set; }
        public Nullable<Single> pendientes { get; set; }
        public Nullable<Int32> programado { get; set; }
        public Nullable<Double> totalutilizados { get; set; }
        public Nullable<Double> diasnogozados { get; set; }
        public Nullable<Double> diasinterrumpidos { get; set; }
        public Nullable<Double> diastrabajados { get; set; }
        public Nullable<Double> diasgozados { get; set; }
        public Nullable<Double> pendienteperiodoanterior { get; set; }
        public Nullable<Double> pagosrealizados { get; set; }
        public Nullable<Single> diastruncos { get; set; }
        public Nullable<Single> pendientediahoy { get; set; }
        public Nullable<Single> saldodisponible { get; set; }
    }
}
