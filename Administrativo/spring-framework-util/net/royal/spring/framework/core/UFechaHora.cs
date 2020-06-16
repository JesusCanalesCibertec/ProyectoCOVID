using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Threading;

namespace net.royal.spring.framework.core
{
    public class UFechaHora
    {
        public static String obtenerNombreScat()
        {
            String cadena = String.Empty;

            cadena = cadena + DateTime.Now.Day + "-";
            cadena = cadena + DateTime.Now.Month + "-";
            cadena = cadena + DateTime.Now.Year + " ";
            cadena = cadena + DateTime.Now.Hour + "-";
            cadena = cadena + DateTime.Now.Minute + "-";
            cadena = cadena + DateTime.Now.Second;
            cadena = cadena + DateTime.Now.ToString("tt");

            return cadena;
        }
        public static Decimal diferenciaMes(DateTime fechaInicio, DateTime fechaFin)
        {
            Decimal resultado = new Decimal(0.0);

            resultado = (fechaFin.Month - fechaInicio.Month) + 12 * (fechaFin.Year - fechaInicio.Year);

            return resultado;
        }
        public static DateTime? fechaCompuesta(DateTime? fecha, DateTime? desde)
        {
            DateTime? fechaRes = fecha;
            fechaRes = new DateTime(fecha.Value.Year, fecha.Value.Month, fecha.Value.Day, desde.Value.Hour, desde.Value.Minute, desde.Value.Second);

            return fechaRes;
        }
        public static Decimal diferenciaHora(DateTime horainicio, DateTime horafin)
        {
            Decimal resultado = new Decimal(0.0);

            resultado = horainicio.Hour - horafin.Hour;

            return resultado;
        }

        public static Decimal diferenciaDia(DateTime fechaInicio, DateTime fechaFin)
        {
            Decimal resultado = new Decimal(0.0);

            TimeSpan ts = fechaFin - fechaInicio;

            resultado = ts.Days;

            return resultado;
        }
        public static DateTime? obtenerFechaHoraInicioDia(DateTime? _fecha)
        {
            if (!_fecha.HasValue)
                return null;
            DateTime f = new DateTime(_fecha.Value.Year, _fecha.Value.Month, _fecha.Value.Day, 0, 0, 0);
            return f;
        }

        public static DateTime? obtenerFechaHoraFinDia(DateTime? _fecha)
        {
            if (!_fecha.HasValue)
                return null;
            DateTime f = new DateTime(_fecha.Value.Year, _fecha.Value.Month, _fecha.Value.Day, 23, 59, 59);
            return f;
        }

        public static String convertirFechaCadena(DateTime? date, String formato)
        {
            String cadena = null;

            if (!date.HasValue)
                return null;

            if (UString.estaVacio(formato))
                return date.Value.ToString();

            return date.Value.ToString(formato);
        }

        public static String obtenerNombreUnico()
        {
            //String cadena = DateTime.Now.ToString("yyyyMMdd-HHmmssfffffff");
            String cadena = Guid.NewGuid().ToString();
            return cadena;
        }
        public static String obtenerEdad(DateTime nacimiento)
        {
            if (nacimiento == null)
            {
                return "Años: 0 Meses: 0 Días: 0";
            }

            DateTime v = nacimiento;

            var now = DateTime.Today;
            var anios = 0;
            var meses = 0;
            var dias = 0;

            if (DateTime.Compare(v, now) > 0)
            {
                return "Años: 0 Meses: 0 Días: 0";
            }

            while (DateTime.Compare(addDate("y", 1, v), now) < 0)
            {
                v = addDate("y", 1, v);
                anios++;

            };
            while (DateTime.Compare(addDate("m", 1, v), now) < 0)
            {
                v = addDate("m", 1, v);
                meses++;
            };

            while (DateTime.Compare(addDate("d", 1, v), now) <= 0)
            {
                v = addDate("d", 1, v);
                dias++;
            };

            return "Años:" + anios + " Meses: " + meses + " Días: " + dias;
        }
        public static DateTime addDate(string pattern, int mount, DateTime fecha)
        {
            var f2 = fecha;

            switch (pattern)
            {
                case "y":
                    return f2.AddYears(mount);
                case "m":
                    return f2.AddMonths(mount);
                case "d":
                    return f2.AddDays(mount);
                default:
                    return f2;
            }
        }
        public static String obtenerFechaHoy()
        {
            return DateTime.Today.ToString("D", new CultureInfo("es-ES"));
        }

        public static string obtenerMesString(int v)
        {
            switch (v)
            {
                case 1:
                    return "Enero";
                case 2:
                    return "Febrero";
                case 3:
                    return "Marzo";
                case 4:
                    return "Abril";
                case 5:
                    return "Mayo";
                case 6:
                    return "Junio";
                case 7:
                    return "Julio";
                case 8:
                    return "Agosto";
                case 9:
                    return "Setiembre";
                case 10:
                    return "Octubre";
                case 11:
                    return "Noviembre";
                case 12:
                    return "Diciembre";
                default:
                    return "";
            }
        }
        public static String obtenerNotNull(Nullable<DateTime> nacimiento)
        {
            if (!nacimiento.HasValue)
            {
                return "";
            }
            else
            {
                return nacimiento.Value.ToString("dd/MM/yyyy");
            }
        }
        public static String obtenerEdadAnios(Nullable<DateTime> nacimiento)
        {
            if (nacimiento.HasValue)
            {
                if (nacimiento == null)
                {
                    return "";
                }

                DateTime v = nacimiento.Value;

                var now = DateTime.Today;
                var anios = 0;


                if (DateTime.Compare(v, now) > 0)
                {
                    return "";
                }

                while (DateTime.Compare(addDate("y", 1, v), now) < 0)
                {
                    v = addDate("y", 1, v);
                    anios++;

                };

                return anios.ToString();
            }
            return "";
        }

        public static String ObtenerA2Digitos(int pValor)
        {
            string cadena2dig = String.Empty;

            if (pValor < 10)
                cadena2dig = "0" + pValor.ToString();
            else
                cadena2dig = pValor.ToString();

            return cadena2dig;
        }
   
    }
}
