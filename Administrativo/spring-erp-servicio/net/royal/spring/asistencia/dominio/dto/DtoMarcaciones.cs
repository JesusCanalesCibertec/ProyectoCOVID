using System;
using System.Collections.Generic;
using System.Text;

namespace net.royal.spring.framework.core.dominio.dto
{
    public class DtoMarcaciones
    {
        public Decimal secuencia { get; set; }
        public Nullable<DateTime> hora { get; set; }
        public Nullable<DateTime> fechaD { get; set; }
        public String observacion { get; set; }
        public String fecha { get; set; }
        public String Dia { get; set; }
        public String HoraIngreso { get; set; }
        public String HoraSalidaRef { get; set; }
        public String HoraRegresoRef { get; set; }
        public String HoraSalida { get; set; }
        public String HoraLaborada { get; set; }
        public Int32 Empleado { get; set; }
        public Decimal EmpleadoD { get; set; }
        public String Nombre { get; set; }
        public String Sede { get; set; }

        public String Semana { get; set; }
        public Nullable<DateTime> desde { get; set; }
        public Nullable<DateTime> hasta { get; set; }
        public Int32 idrow { get; set; }
        public String Lunes { get; set; }
        public String Martes { get; set; }
        public String Miercoles { get; set; }
        public String Jueves { get; set; }
        public String Viernes { get; set; }
        public String Sabado { get; set; }
        public String Domingo { get; set; }
        public String HorasSemana { get; set; }
        public String HorasExtras { get; set; }
        public String HorasDebe { get; set; }
        public String HorasFavor { get; set; }
        public String HorasExtrasAcumuladas { get; set; }
        public String HorasDebeAcumuladas { get; set; }
        public String HorasExtrasRango { get; set; }
        public String HorasDebeRango { get; set; }

        public String HorasCarga { get; set; }
        public String NombreJefe { get; set; }
        public String Area { get; set; }

        public String HorasNoAutorizadas { get; set; }
        public String HorasAutorizadas { get; set; }

        public DtoMarcaciones marcas { get; set; }

        public String puestoNombre { get; set; }


        public String HorasComp { get; set; }
        public String HorasNoAut { get; set; }
        public String HorasTard { get; set; }
        public String HorasSalAnt { get; set; }

    }
}
