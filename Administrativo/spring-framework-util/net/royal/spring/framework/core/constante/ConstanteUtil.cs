using System;
using System.Collections.Generic;
using System.Text;

namespace net.royal.spring.framework.constante
{
    public static class ConstanteUtil
    {

        public static String UFECHAHORA_FORMATO_FECHA = "dd/MM/yyyy";

        public static String UFECHAHORA_FORMATO_FECHA_HORA = "dd/MM/yyyy hh:mm:ss";

        public enum TipoDato
        {
            String,
            Date,
            DateTime,
            Integer,
            Decimal,
            ArrayInteger
        }
    }
}
