using System;
using System.Collections.Generic;
using System.Text;

namespace net.royal.spring.framework.core
{
    public class UInteger
    {
        public static Int32? obtenerValorEnteroSinNulo(Int32? variable)
        {
            if (!variable.HasValue)
                return 0;
            return variable;
        }
        public static Int32? obtenerValorEntero(Decimal? numero)
        {
            if (numero == null)
            {
                return 0;
            }
            return Convert.ToInt32(numero);
        }
        public static Int32? obtenerValor(Int32? numero)
        {
            if (numero == null)
            {
                return 0;
            }
            return numero;
        }

        public static Boolean esCeroOrNulo(Int32? numero)
        {
            if (numero == null)
            {
                return true;
            }
            if (numero == 0)
            {
                return true;
            }
            return false;
        }
        public static Boolean esCeroOrNulo(Double? numero)
        {
            if (!numero.HasValue)
            {
                return true;
            }
            if (numero == null)
            {
                return true;
            }
            if (numero == 0)
            {
                return true;
            }
            return false;
        }
    }
}
