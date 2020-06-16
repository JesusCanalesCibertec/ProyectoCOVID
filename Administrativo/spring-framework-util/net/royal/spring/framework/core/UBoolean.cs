using System;
using System.Collections.Generic;
using System.Text;

namespace net.royal.spring.framework.core
{
    public class UBoolean
    {
        public static String validarBoolean(Boolean? condicion) {
            if (condicion == null)
                return "N";
            if (condicion == false)
                return "N";
            return "S";
        }
        public static Boolean validarFlag(String condicion)
        {
            Boolean condicionBoolean = false;
            if (condicion != null)
            {
                if (condicion.Trim().Equals("S") || condicion.Equals("on"))
                {
                    condicionBoolean = true;
                }
            }
            return condicionBoolean;
        }
    }
}
